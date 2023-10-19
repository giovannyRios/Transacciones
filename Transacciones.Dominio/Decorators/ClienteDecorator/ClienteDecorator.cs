using Microsoft.Extensions.Caching.Memory;
using Transacciones.Dominio.Context;
using Transacciones.Dominio.Repository.Interfaces;

namespace Transacciones.Dominio.Decorators.ClienteDecorator
{
    public class ClienteDecorator : IclienteDecorator
    {
        private readonly IclienteRepository _clienteRepository;
        private readonly IMemoryCache _cache;

        public ClienteDecorator(IclienteRepository clienteRepository, IMemoryCache memoryCache)
        {
            _clienteRepository = clienteRepository;
            _cache = memoryCache;
        }

        public async Task<Cliente> ObtenerClientePorId(int Id)
        {
            string key = $"member-{Id}";
            return await _cache.GetOrCreateAsync(key,
                    async entry =>
                    {
                        entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                        return await _clienteRepository.ObtenerClientePorId(Id);
                    });
        }

        public async Task<Cliente> ObtenerClientePorIdentificacion(string numeroIdentificacion)
        {
            string key = $"member-{numeroIdentificacion}";
            return await _cache.GetOrCreateAsync(key,
                    async entry =>
                    {
                        entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                        return await _clienteRepository.ObtenerClientePorIdentificacion(numeroIdentificacion);
                    });
        }

    }
}
