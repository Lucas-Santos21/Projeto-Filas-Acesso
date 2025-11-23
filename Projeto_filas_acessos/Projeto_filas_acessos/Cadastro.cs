using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Projeto_filas_acessos
{
    class Cadastro
    {
        //propriedades
        private List<Usuario> _usuarios;
        private List<Ambiente> _ambientes;

        private readonly string _conexao = "Data Source=localhost;Initial Catalog=ControleAcessos;Integrated Security=True";

        //Construtores
        public Cadastro()
        {
            Usuarios = new List<Usuario>();
            Ambientes = new List<Ambiente>();
        }

        //Getters e Setters

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }

        public List<Ambiente> Ambientes
        {
            get { return _ambientes; }
            set { _ambientes = value; }
        }

        //Metodos Publicos
        public void adicionarUsuario(Usuario usuario)
        {
            if (pesquisarUsuario(usuario) != null)
            {
                Console.WriteLine("Usuario já existe!");
                return;
            }

            Usuarios.Add(usuario);
            Console.WriteLine("Usuario adicionado!");
        }

        public bool removerUsuario(Usuario usuario)
        {
            Usuario encontrado = pesquisarUsuario(usuario);

            if (encontrado == null)
            {
                return false;
            }

            if (encontrado.Ambientes.Count > 0)
            {
                Console.WriteLine("O usuario não pode ser removido pois possue permições");
                return false;
            }

            Usuarios.Remove(encontrado);
            Console.WriteLine("Usuario Removido!");
            return true;
        }

        public Usuario pesquisarUsuario(Usuario usuario)
        {
            foreach (Usuario u in Usuarios)
            {
                if (u.Id == usuario.Id)
                {
                    return u;
                }
            }

            return null;
        }

        public void adicionarAmbiente(Ambiente ambiente)
        {
            if (pesquisarAmbiente(ambiente) != null)
            {
                Console.WriteLine("Ambiente já existe!");
                return;
            }

            Ambientes.Add(ambiente);
            Console.WriteLine("Ambiente adicionado!");
        }

        public bool removerAmbiente(Ambiente ambiente)
        {
            Ambiente encontrado = pesquisarAmbiente(ambiente);

            if (encontrado == null)
            {
                return false;
            }

            Ambientes.Remove(encontrado);
            Console.WriteLine("Ambiente Removido!");
            return true;
        }

        public Ambiente pesquisarAmbiente(Ambiente ambiente)
        {
            foreach (Ambiente a in Ambientes)
            {
                if (a.Id == ambiente.Id)
                {
                    return a;
                }
            }

            return null;
        }

        public void upload()
        {
            using (SqlConnection con = new SqlConnection(_conexao))
            {
                con.Open();

                // LIMPAR TABELAS
                new SqlCommand("DELETE FROM LogAcesso", con).ExecuteNonQuery();
                new SqlCommand("DELETE FROM Permissao", con).ExecuteNonQuery();
                new SqlCommand("DELETE FROM Usuario", con).ExecuteNonQuery();
                new SqlCommand("DELETE FROM Ambiente", con).ExecuteNonQuery();

                // 1 — INSERIR AMBIENTES
                foreach (var a in _ambientes)
                {
                    var cmd = new SqlCommand(
                        "INSERT INTO Ambiente(id, nome) VALUES (@id, @nome)",
                        con
                    );

                    cmd.Parameters.AddWithValue("@id", a.Id);
                    cmd.Parameters.AddWithValue("@nome", a.Nome);
                    cmd.ExecuteNonQuery();

                    // INSERIR LOGS DO AMBIENTE
                    foreach (var log in a.Logs)
                    {
                        var cmdLog = new SqlCommand(
                            @"INSERT INTO LogAcesso(dtAcesso, usuarioId, ambienteId, tipoAcesso)
                      VALUES (@dt, @u, @a, @t)",
                            con
                        );

                        cmdLog.Parameters.AddWithValue("@dt", log.dtAcesso);
                        cmdLog.Parameters.AddWithValue("@u", log.usuario.Id);
                        cmdLog.Parameters.AddWithValue("@a", a.Id);
                        cmdLog.Parameters.AddWithValue("@t", log.tipoAcesso);

                        cmdLog.ExecuteNonQuery();
                    }
                }

                // 2 — INSERIR USUÁRIOS
                foreach (var u in _usuarios)
                {
                    var cmd = new SqlCommand(
                        "INSERT INTO Usuario(id, nome) VALUES (@id, @nome)",
                        con
                    );

                    cmd.Parameters.AddWithValue("@id", u.Id);
                    cmd.Parameters.AddWithValue("@nome", u.Nome);
                    cmd.ExecuteNonQuery();

                    // 3 — INSERIR PERMISSÕES DESSE USUÁRIO
                    foreach (var amb in u.Ambientes)
                    {
                        var cmdPerm = new SqlCommand(
                            "INSERT INTO Permissao(usuarioId, ambienteId) VALUES (@u, @a)",
                            con
                        );

                        cmdPerm.Parameters.AddWithValue("@u", u.Id);
                        cmdPerm.Parameters.AddWithValue("@a", amb.Id);
                        cmdPerm.ExecuteNonQuery();
                    }
                }
            }
        }

        public void download()
        {
            using (SqlConnection con = new SqlConnection(_conexao))
            {
                con.Open();

                _usuarios.Clear();
                _ambientes.Clear();

                SqlDataReader reader;

                // 1 — CARREGAR AMBIENTES
                var cmdAmb = new SqlCommand("SELECT id, nome FROM Ambiente", con);
                reader = cmdAmb.ExecuteReader();

                while (reader.Read())
                {
                    _ambientes.Add(new Ambiente(reader.GetInt32(0), reader.GetString(1)));
                }
                reader.Close();

                // 2 — CARREGAR USUÁRIOS
                var cmdUsuarios = new SqlCommand("SELECT id, nome FROM Usuario", con);
                reader = cmdUsuarios.ExecuteReader();

                while (reader.Read())
                {
                    _usuarios.Add(new Usuario(reader.GetInt32(0), reader.GetString(1)));
                }
                reader.Close();

                // 3 — CARREGAR PERMISSÕES
                var cmdPerm = new SqlCommand("SELECT usuarioId, ambienteId FROM Permissao", con);
                reader = cmdPerm.ExecuteReader();

                while (reader.Read())
                {
                    int uId = reader.GetInt32(0);
                    int aId = reader.GetInt32(1);

                    var u = _usuarios.Find(x => x.Id == uId);
                    var a = _ambientes.Find(x => x.Id == aId);

                    if (u != null && a != null)
                        u.concederPermissao(a);
                }
                reader.Close();

                // 4 — CARREGAR LOGS
                var cmdLogs = new SqlCommand(
                    "SELECT dtAcesso, usuarioId, ambienteId, tipoAcesso FROM LogAcesso",
                    con
                );

                reader = cmdLogs.ExecuteReader();

                while (reader.Read())
                {
                    DateTime dt = reader.GetDateTime(0);
                    int uId = reader.GetInt32(1);
                    int aId = reader.GetInt32(2);
                    bool tipo = reader.GetBoolean(3);

                    var user = _usuarios.Find(x => x.Id == uId);
                    var amb = _ambientes.Find(x => x.Id == aId);

                    if (user != null && amb != null)
                    {
                        amb.registrarLog(new Log(dt, user, tipo));
                    }
                }
                reader.Close();
            }
        }
    }
}
