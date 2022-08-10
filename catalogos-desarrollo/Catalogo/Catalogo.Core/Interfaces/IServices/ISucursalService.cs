﻿using Catalogo.Core.DTOs;
using System.Threading.Tasks;

namespace Catalogo.Core.Interfaces.IServices
{
    public interface ISucursalService
    {
        Task<Respuesta> ObtenerSucursales();
    }
}
