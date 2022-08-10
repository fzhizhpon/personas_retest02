using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personas.Core.Interfaces.IRepositories
{
    public interface ILogsRepository<T>
    {

        void Informativo(string mensaje);
        void Error(string mensaje);
        void Warning (string mensaje);
    }
}
