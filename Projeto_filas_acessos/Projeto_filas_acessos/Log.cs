using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_filas_acessos
{
    internal class Log
    {
        //Propriedades
        private DateTime _dtAcesso;
        private Usuario _usuario;
        private bool _tipoAcesso;

        //Construtores

        public Log( Usuario usuario, bool tipoAcesso)
        {
            DtAcesso = DateTime.Now;
            Usuario = usuario;
            TipoAcesso = tipoAcesso;
        }

        //Getters e Setters
        public DateTime DtAcesso {

            get { return _dtAcesso; }
            set { _dtAcesso = value; }
        }

        public Usuario Usuario {

            get { return _usuario; }
            set { _usuario = value; }
        }

        public bool TipoAcesso {

            get { return _tipoAcesso; }
            set { _tipoAcesso = value; }
        }

    }
}
