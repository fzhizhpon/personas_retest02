using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.DataBase
{
    public interface IConexion<T>
    {
        public T ObtenerConexion();
    }
}
