using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_filas_acessos
{
    internal class Ambiente
    {
        //Propriedades
        private int _id;
        private string _nome;
        private Queue<Log> _logs;

        //Construtor
        public Ambiente(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Logs = new Queue<Log>();
        }

        //Getter e Setter
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

        public Queue <Log> Logs
        {
            get { return _logs; }
            set { _logs = value; }
        }

        //Método

        public void registrarLog(Log log)
        { 
            if (log == null)
            {
                Console.WriteLine("O log recebido é nulo");
                return;
            }
            if(Logs.Count >= 100)
            {
                Console.WriteLine("Quantidade maxima de logs no ambiente atingida!");
                return;
            }
            Logs.Enqueue(log);
        }
    }
}
