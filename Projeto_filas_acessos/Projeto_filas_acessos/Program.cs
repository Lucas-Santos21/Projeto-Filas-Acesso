﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_filas_acessos
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Cadastro cadastro = new Cadastro();

            //Carregando dados
            cadastro.download();

            int opc = -1;

            while (opc != 0)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Cadastrar ambiente");
                Console.WriteLine("2. Consultar ambiente");
                Console.WriteLine("3. Excluir ambiente");
                Console.WriteLine("4. Cadastrar usuário");
                Console.WriteLine("5. Consultar usuário");
                Console.WriteLine("6. Excluir usuário");
                Console.WriteLine("7. Conceder permissão");
                Console.WriteLine("8. Revogar permissão");
                Console.WriteLine("9. Registrar acesso");
                Console.WriteLine("10. Consultar logs");
                Console.WriteLine("0. Sair");
                Console.Write("Selecione: ");

                int.TryParse(Console.ReadLine(), out opc);
                Console.Clear();

                switch (opc)
                {
                    // 1 — Cadastrar ambiente
                    case 1:
                        {
                            Console.Write("ID do ambiente: ");
                            int idA = int.Parse(Console.ReadLine());

                            Console.Write("Nome: ");
                            string nomeA = Console.ReadLine();

                            cadastro.adicionarAmbiente(new Ambiente(idA, nomeA));
                            break;
                        }

                    // 2 — Consultar ambiente
                    case 2:
                        {
                            Console.Write("ID do ambiente: ");
                            int idAc = int.Parse(Console.ReadLine());

                            var ambC = cadastro.pesquisarAmbiente(new Ambiente(idAc, ""));

                            if (ambC == null)
                            {
                                Console.WriteLine("Ambiente não encontrado.");
                            }
                            else
                            {
                                Console.WriteLine($"ID: {ambC.Id}  Nome: {ambC.Nome}");
                            }

                            break;
                        }

                    // 3 — Remover ambiente
                    case 3:
                        {
                            Console.Write("ID do ambiente: ");
                            int idAr = int.Parse(Console.ReadLine());

                            cadastro.removerAmbiente(new Ambiente(idAr, ""));
                            break;
                        }

                    // 4 — Cadastrar usuário
                    case 4:
                        {
                            Console.Write("ID do usuário: ");
                            int idU = int.Parse(Console.ReadLine());

                            Console.Write("Nome: ");
                            string nomeU = Console.ReadLine();

                            cadastro.adicionarUsuario(new Usuario(idU, nomeU));
                            break;
                        }

                    // 5 — Consultar usuário
                    case 5:
                        {
                            Console.Write("ID do usuário: ");
                            int idUc = int.Parse(Console.ReadLine());

                            var userC = cadastro.pesquisarUsuario(new Usuario(idUc, ""));

                            if (userC == null)
                            {
                                Console.WriteLine("Usuário não encontrado.");
                            }
                            else
                            {
                                Console.WriteLine($"ID: {userC.Id}  Nome: {userC.Nome}");
                                Console.WriteLine("Permissões:");

                                foreach (var amb in userC.Ambientes)
                                {
                                    Console.WriteLine($"- {amb.Id} ({amb.Nome})");
                                }
                            }

                            break;
                        }

                    // 6 — Remover usuário
                    case 6:
                        {
                            Console.Write("ID do usuário: ");
                            int idUr = int.Parse(Console.ReadLine());

                            cadastro.removerUsuario(new Usuario(idUr, ""));
                            break;
                        }

                    // 7 — Conceder permissão
                    case 7:
                        {
                            Console.Write("ID do usuário: ");
                            int idUp = int.Parse(Console.ReadLine());

                            Console.Write("ID do ambiente: ");
                            int idAp = int.Parse(Console.ReadLine());

                            var usuarioP = cadastro.pesquisarUsuario(new Usuario(idUp, ""));
                            var ambienteP = cadastro.pesquisarAmbiente(new Ambiente(idAp, ""));

                            if (usuarioP == null || ambienteP == null)
                            {
                                Console.WriteLine("Usuário ou ambiente não encontrado.");
                            }
                            else
                            {
                                if (usuarioP.concederPermissao(ambienteP))
                                {
                                    Console.WriteLine("Permissão concedida!");
                                }
                                else
                                {
                                    Console.WriteLine("Permissão já existente.");
                                }
                            }

                            break;
                        }

                    // 8 — Revogar permissão
                    case 8:
                        {
                            Console.Write("ID do usuário: ");
                            int idUrv = int.Parse(Console.ReadLine());

                            Console.Write("ID do ambiente: ");
                            int idArv = int.Parse(Console.ReadLine());

                            var usuarioRv = cadastro.pesquisarUsuario(new Usuario(idUrv, ""));
                            var ambienteRv = cadastro.pesquisarAmbiente(new Ambiente(idArv, ""));

                            if (usuarioRv == null || ambienteRv == null)
                            {
                                Console.WriteLine("Usuário ou ambiente não encontrado.");
                            }
                            else
                            {
                                if (usuarioRv.revogarPermissao(ambienteRv))
                                {
                                    Console.WriteLine("Permissão revogada!");
                                }
                                else
                                {
                                    Console.WriteLine("Este usuário não tem permissão neste ambiente.");
                                }
                            }

                            break;
                        }

                    // 9 — Registrar acesso
                    case 9:
                        {
                            Console.Write("ID do usuário: ");
                            int idUlog = int.Parse(Console.ReadLine());

                            Console.Write("ID do ambiente: ");
                            int idAlog = int.Parse(Console.ReadLine());

                            var ulog = cadastro.pesquisarUsuario(new Usuario(idUlog, ""));
                            var alog = cadastro.pesquisarAmbiente(new Ambiente(idAlog, ""));

                            if (ulog == null || alog == null)
                            {
                                Console.WriteLine("Usuário ou ambiente não encontrado.");
                                break;
                            }

                            bool permitido = ulog.Ambientes.Contains(alog);

                            alog.registrarLog(new Log(ulog, permitido));

                            if (permitido == true)
                            {
                                Console.WriteLine("Acesso autorizado");
                            }
                            else
                            {
                                Console.WriteLine("Acesso NEGADO");
                            }

                            break;
                        }

                    // 10 — Consultar logs
                    case 10:
                        {
                            Console.Write("ID do ambiente: ");
                            int idLog = int.Parse(Console.ReadLine());

                            var ambienteLog = cadastro.pesquisarAmbiente(new Ambiente(idLog, ""));

                            if (ambienteLog == null)
                            {
                                Console.WriteLine("Ambiente não encontrado.");
                                break;
                            }

                            Console.WriteLine("1 - Só autorizados");
                            Console.WriteLine("2 - Só negados");
                            Console.WriteLine("3 - Todos");
                            Console.Write("Filtro: ");
                            int filtro = int.Parse(Console.ReadLine());

                            foreach (var log in ambienteLog.Logs)
                            {
                                if (filtro == 1 && log.TipoAcesso == false)
                                {
                                    continue;
                                }

                                if (filtro == 2 && log.TipoAcesso == true)
                                {
                                    continue;
                                }

                                Console.WriteLine(
                                    $"{log.DtAcesso} - Usuário: {log.Usuario.Nome} - {(log.TipoAcesso ? "OK" : "NEGADO")}"
                                );
                            }

                            break;
                        }

                    // 0 — Sair
                    case 0:
                        {
                            Console.WriteLine("Salvando dados...");
                            cadastro.upload();
                            Console.WriteLine("Encerrado.");
                            break;
                        }

                    // Opção inválida
                    default:
                        {
                            Console.WriteLine("Opção inválida.");
                            break;
                        }
                }
            }

        }
    }
}
