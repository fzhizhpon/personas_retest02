using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Dtos
{
    public class HistoricoDto<T>
    {
        public string guid { get; set; }

        // El nombre del módulo será el nombre de las base de datos
        public string modulo { get; set; }
        // El nombre de la tabla será el nombre de la colección
        public string tabla { get; set; }
        public T referencia { get; set; }
    }

}