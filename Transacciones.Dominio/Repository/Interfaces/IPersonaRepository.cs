using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;

namespace Transacciones.Dominio.Repository.Interfaces
{
    public interface IPersonaRepository
    {
        public Task<bool> AdicionarPersona(Persona persona);

        public Task<bool> EliminarPersona(Persona persona);

        public Task<Persona> ObtenerPersonaPorIdentificacion(string numeroIdentificacion);

        public Task<Persona> ObtenerPersonaPorId(int Id);

        public Task<List<Persona>> ObtenerPersonas();

        public Task<bool> ActualizarPersona(Persona persona);

    }
}
