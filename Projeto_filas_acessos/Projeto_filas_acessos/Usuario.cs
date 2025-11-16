using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_filas_acessos
{
    internal class Usuario
    {
        private int _id;
        private string _nome;
        private List<Ambiente> ambientes;

        //Construtor


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
            get { return ambientes; }    
        }

        //M[etodo
        public bool ConcederPermissao(Ambiente ambientes)
        {
            if (ambientes == null)
            {
                throw new ArgumentNullException(nameof(ambientes));
            }

            if (Ambientes.Contains(ambientes))
            {
                return false;
            }

            Ambientes.Add(ambientes);
            return true;
        }

        public bool RevogarPermissoes(Ambiente ambientes)
        {
            if (ambientes == null)
            {
                throw new ArgumentNullException(nameof(ambientes));
            }

            return (Ambientes.Remove(ambientes));
        }
    }
}
