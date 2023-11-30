using Microsoft.Extensions.DependencyInjection;
using Transacciones.Dominio.Decorators.ClienteDecorator;
using Transacciones.Dominio.Repository.Implements;
using Transacciones.Dominio.Repository.Interfaces;

namespace Transacciones.Dominio.IoCRegister
{
    public class IoCRegister
    {
        public static IServiceCollection GetConfiguration(IServiceCollection builder)
        {
            //Inyecta las dependencias de los repositorios para el proyecto Transacciones.Negocio
            builder.AddScoped<IclienteRepository, ClienteRepository>();
            builder.AddScoped<IclienteDecorator, ClienteDecorator>(); 

            builder.AddScoped<IcuentaRepository, CuentaRepository>();
            builder.AddScoped<IMovimientosRepository, MovimientoRepository>();
            builder.AddScoped<ITipoCuentaRepository, TipoCuentaRepository>();
            builder.AddScoped<IGeneroRepository, GeneroRepository>();
            return builder;
        }
    }
}
