﻿using Catalogo.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IRepositories
{
    public interface ICaracteristicaCuentaRepository
    {
        Task<(int, IEnumerable<ComboStringDto>)> SelectCaracteristicaCuentas();
    }
}
