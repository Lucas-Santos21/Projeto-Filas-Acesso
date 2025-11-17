using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_filas_acessos
{
    internal class Usuario
    {
        //Propriedades
        private int _id;
        private string _nome;
        private List<Ambiente> _ambientes;

        //Construtor
        public Usuario()
        {
            Id = 0;
            Nome = "";
            Ambientes = new List<Ambiente>();
        }

        public Usuario(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Ambientes = new List<Ambiente>();
        }

        //Getters e Setters

        public int Id
        { 
            get { return _id; } 
            set { _id = value; } 
        }

        public string Nome 
        { 
            get { return _nome; } 
            set { _nome = value; } 
        }

        public List<Ambiente> Ambientes
        {
            get { return _ambientes; }
            set { _ambientes = value; }
        }

        //Metodos
        public bool ConcederPermissao(Ambiente ambientes)
        {
            if (ambientes == null)
            {
                return false;
            }

            if (Ambientes.Contains(ambientes))
            {
                return false;
            }

            Ambientes.Add(ambientes);
            return true;
        }

        public bool RevogarPermissoes(Ambiente ambiente)
        {
            if (ambiente == null)
            {
                return false;
            }

            if (Ambientes.Contains(ambiente))
            {
                Ambientes.Remove(ambiente);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
