﻿using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ISubSectorEconomicoRepository
    {
        Task<(int, IEnumerable<ComboStringDto>)> SelectSubSectoresEconomicosPorSector(ObtenerSubSectorDto dto);
    }
}
