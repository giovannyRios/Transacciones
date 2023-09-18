using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public PersonaService(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }
        public async Task<bool> ActualizarPersona(PersonaDTO personaDTO)
        {
            bool resultado = false;

            Persona persona = await _personaRepository.ObtenerPersonaPorId(personaDTO.Id);
            if (persona != null)
            {
                resultado = await _personaRepository.ActualizarPersona(_mapper.Map<Persona>(personaDTO));
            }

            return resultado;

        }

        public async Task<bool> AdicionarPersona(PersonaDTO personaDTO)
        {
            bool resultado = false;
            Persona persona = await _personaRepository.ObtenerPersonaPorId(personaDTO.Id);
            if (persona == null)
            {
                Persona personaAdicional = _mapper.Map<Persona>(personaDTO);
                resultado = await _personaRepository.AdicionarPersona(personaAdicional);
            }
            return resultado;

        }

        public async Task<bool> eliminarPersona(PersonaDTO personaDTO)
        {
            bool resultado = false;

            Persona persona = await _personaRepository.ObtenerPersonaPorId(personaDTO.Id);
            if (persona != null)
            {
                resultado = await _personaRepository.EliminarPersona(persona);
            }

            return resultado;

        }

        public async Task<PersonaDTO> ObtenerPersonaPorId(int Id)
        {
            Persona persona = await _personaRepository.ObtenerPersonaPorId(Id);
            PersonaDTO personaDTO = new PersonaDTO();
            if (persona != null)
            {
                personaDTO = _mapper.Map<PersonaDTO>(persona);
            }

            return personaDTO;
        }

        public async Task<PersonaDTO> ObtenerPersonaPorNumeroIdentificacion(string numeroIdentificacion)
        {
            Persona persona = await _personaRepository.ObtenerPersonaPorIdentificacion(numeroIdentificacion);
            PersonaDTO personaDTO = new PersonaDTO();
            if (persona != null)
            {
                personaDTO = _mapper.Map<PersonaDTO>(persona);
            }

            return personaDTO;
        }

        public async Task<List<PersonaDTO>> obtenerPersonas()
        {

            List<Persona> personas = await _personaRepository.ObtenerPersonas();
            List<PersonaDTO> personasDTO = new List<PersonaDTO>();
            if (personas != null)
            {
                personasDTO.AddRange(_mapper.Map<List<PersonaDTO>>(personas));
            }

            return personasDTO;

        }
    }
}
