using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMensajesEventHub
{
    public class MensajeUsuario
    {
        public int IdUSuario { get; set; }
        public String Ciudad { get; set; }

        public String Localidad { get; set; }

        public override string ToString()
        {
            return "El usuario " + this.IdUSuario
                + " se ha conectado en "
                + this.Ciudad + " desde " + this.Localidad;
        }

    }
}
