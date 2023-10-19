using AutoMapper;
using Transacciones.Dominio.Repository.Interfaces;
using Transacciones.Negocio.DTO;
using Transacciones.Negocio.Services.Interfaces;

namespace Transacciones.Negocio.Services.Implements
{
    public class GenerosServices : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;
        public GenerosServices(IGeneroRepository generoRepository, IMapper mapper)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
        }
        public async Task<GeneroDTO> ObtenerGeneroPorId(int Id)
        {
            var genero = await _generoRepository.ObtenerGeneroPorId(Id);
            if (genero != null)
                return _mapper.Map<GeneroDTO>(genero);
            return null;
        }

        public async Task<GeneroDTO> ObtenerGeneroPorValor(string Valor)
        {
            var genero = await _generoRepository.ObtenerGeneroPorValor(Valor);
            if (genero != null)
                return _mapper.Map<GeneroDTO>(genero);
            return null;
        }

        public async Task<List<GeneroDTO>> ObtenerGeneros()
        {
            List<GeneroDTO> generoDTOs = new List<GeneroDTO>();
            var listaGeneros = await _generoRepository.ObtenerGeneros();
            if (listaGeneros != null)
            {
                generoDTOs.AddRange(_mapper.Map<List<GeneroDTO>>(listaGeneros));
                return generoDTOs;
            }
            else
            {
                return null;
            }


        }
    }
}
