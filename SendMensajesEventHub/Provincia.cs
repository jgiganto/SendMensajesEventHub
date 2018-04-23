using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMensajesEventHub
{
    public class Provincia
    {
        public int IdProvincia { get; set; }
        public String Nombre { get; set; }
        public List<String> Localidades { get; set; }

    }
}
