using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_filas_acessos
{
    internal class Ambiente
    {
        private int _id;
        private string _nome;
        private Queue<Log> logs;
        private const int _maxLogs = 100;

        //Construtor
        public Ambiente(int id, string nome)
        {
            Id = id;
            Nome = nome;
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
            get { return logs; }
        }

        //Método

        public void registrarLog(Log log)
        { 
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }
            if(Logs.Count >= _maxLogs)
            {
                Logs.Dequeue();
            }
            Logs.Enqueue(log);
        }
    }
}
