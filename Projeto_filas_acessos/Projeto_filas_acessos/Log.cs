using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_filas_acessos
{
    internal class Log
    {
        private DateTime _dtAcesso;
        private Usuario _usuario;
        private bool _tipoAcesso;

        //Construtores
        public Log()
        {

        }

        public Log(DateTime dtAcesso, Usuario usuario, bool tipoAcesso)
        {
            DtAcesso = dtAcesso;
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

        public string ObterTipoAcesso()
        {
            return TipoAcesso ? "Autorizado" : "Negado";
        }
    }
}
