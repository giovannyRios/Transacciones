using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Repository.Interfaces
{
    public interface IGeneroRepository
    {
        public bool AdicionarGenero(Genero genero);

        public bool RemoverGenero(Genero genero);

        public Task<Genero> ObtenerGeneroPorId(int Id);

        public Task<Genero> ObtenerGeneroPorValor(string valor);

        public Task<List<Genero>> ObtenerGeneros();

        public bool ActualizarGenero(Genero genero);


    }
}
