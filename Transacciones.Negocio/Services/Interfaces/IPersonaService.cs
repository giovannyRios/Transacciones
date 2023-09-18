using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;
using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.Services.Interfaces
{
    public interface IPersonaService
    {
        public Task<bool> AdicionarPersona(PersonaDTO personaDTO);

        Task<bool> ActualizarPersona(PersonaDTO personaDTO);

        Task<PersonaDTO> ObtenerPersonaPorNumeroIdentificacion(string numeroIdentificacion);

        Task<PersonaDTO> ObtenerPersonaPorId(int Id);

        Task<List<PersonaDTO>>  obtenerPersonas();

        Task<bool> eliminarPersona(PersonaDTO personaDTO);
    }
}
