using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.App
{
    public class ConexionDb
    {
        public string Host { get; set; }
        public string Puerto { get; set; }
        public string NombreServicio { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }
}
