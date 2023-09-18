using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.Services.Interfaces
{
    public interface IGeneroService
    {
        public Task<GeneroDTO> ObtenerGeneroPorId(int Id);
        public Task<GeneroDTO> ObtenerGeneroPorValor(string Valor);
        public Task<List<GeneroDTO>> ObtenerGeneros();
    }
}
