using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;
using System.Configuration;
using Microsoft.ServiceBus.Messaging;

namespace SendMensajesEventHub
{
    class Program
    {
        static List<Provincia> GetProvincias()
        {
            Stream stream =
                Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("SendMensajesEventHub.provincias.xml");
            XDocument doc =
                XDocument.Load(stream);
            var consulta = from datos in doc.Descendants("provincia")
                           select new Provincia
                           {
                               IdProvincia = int.Parse(datos.Attribute("id").Value),
                               Nombre = datos.Element("nombre").Value,
                               Localidades = new List<string>
                               (from loc in datos.Descendants("localidad")
                                select loc.Value)
                           };
            return consulta.ToList();
        }
        static MensajeUsuario GenerarMensajes(List<Provincia> provincias)
        {//emulador de menajes
            Random rdn = new Random();
            int idusuario = rdn.Next(1, 255);
            int idprovincia = rdn.Next(provincias.Count);
            int numlocalidades = provincias[idprovincia].Localidades.Count;
            int idlocalidad = rdn.Next(numlocalidades);
            MensajeUsuario mensaje = new MensajeUsuario
            {
                IdUSuario = idusuario,
                Ciudad = provincias[idprovincia].Nombre,
                Localidad = provincias[idprovincia].Localidades[idlocalidad]
            };
            return mensaje;
        }
        static void Main(string[] args)
        {
            String claves =
                ConfigurationManager.AppSettings["eventhub"];
            String nombreeventhub = "centromensajestajamar";
            EventHubClient cliente =
                EventHubClient.CreateFromConnectionString(claves, nombreeventhub);
            List<Provincia> provincias = GetProvincias();
            while (true)
            {
                MensajeUsuario mensaje = GenerarMensajes(provincias);
                byte[] datos = Encoding.UTF8.GetBytes(mensaje.ToString());
                EventData evento = new EventData(datos);
                cliente.Send(evento);
                //pintamos los datos
                Console.WriteLine(mensaje.ToString());
                Thread.Sleep(1000);
            }


        }
    }
}
