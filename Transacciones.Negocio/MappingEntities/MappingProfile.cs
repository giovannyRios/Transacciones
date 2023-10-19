using AutoMapper;
using Transacciones.Dominio.Context;
using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.MappingEntities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Cliente
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(src => src.Estado)
                )
                .ForMember(
                    dest => dest.Contrasena,
                    opt => opt.MapFrom(src => src.Contrasena)
                ).ForMember(
                    dest => dest.Identificacion,
                    opt => opt.MapFrom(src => src.Identificacion)
                )
                .ForMember(
                    dest => dest.Edad,
                    opt => opt.MapFrom(src => src.Edad)
                )
                .ForMember(
                    dest => dest.Telefono,
                    opt => opt.MapFrom(src => src.Telefono)
                )
                .ForMember(
                    dest => dest.Direccion,
                    opt => opt.MapFrom(src => src.Direccion)
                )
                .ForMember(
                    dest => dest.GeneroId,
                    opt => opt.MapFrom(src => src.GeneroId)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                .ForMember(
                    dest => dest.FechaActualizacion,
                    opt => opt.MapFrom(src => src.FechaActualizacion)
                )
                .ReverseMap();

            //Cuenta
            CreateMap<Cuenta, CuentaDTO>()
                .ForMember(
                    dest => dest.TipoCuentaId,
                    opt => opt.MapFrom(src => src.TipoCuentaId)
                )
                .ForMember(
                    dest => dest.NumeroCuenta,
                    opt => opt.MapFrom(src => src.NumeroCuenta)
                )
                .ForMember(
                    dest => dest.ClienteId,
                    opt => opt.MapFrom(src => src.ClienteId)
                )
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(src => src.Estado)
                )
                .ForMember(
                    dest => dest.Saldo,
                    opt => opt.MapFrom(src => src.Saldo)
                )
                .ForMember(
                    dest => dest.FechaCreacion,
                    opt => opt.MapFrom(src => src.FechaCreacion)
                )
                .ForMember(
                    dest => dest.FechaActualizacion,
                    opt => opt.MapFrom(src => src.FechaActualizacion)
                )
                .ReverseMap();

            //Genero
            CreateMap<Genero, GeneroDTO>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Valor,
                    opt => opt.MapFrom(src => src.Valor)
                ).ReverseMap();

            //Movimiento
            CreateMap<Movimiento, MovimientoDTO>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.CuentaId,
                    opt => opt.MapFrom(src => src.CuentaId)
                )
                .ForMember(
                    dest => dest.Saldo,
                    opt => opt.MapFrom(src => src.Saldo)
                )
                .ForMember(
                    dest => dest.DescripcionMovimiento,
                    opt => opt.MapFrom(src => src.DescripcionMovimiento)
                )
                .ForMember(
                    dest => dest.FechaMovimiento,
                    opt => opt.MapFrom(src => src.FechaMovimiento)
                ).ReverseMap();
            //Tipo cuenta
            CreateMap<TipoCuentum, TipoCuentaDTO>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Valor,
                    opt => opt.MapFrom(src => src.Valor)
                )
                .ForMember(
                    dest => dest.Estado,
                    opt => opt.MapFrom(src => src.Estado)
                ).ReverseMap();

        }

    }
}
