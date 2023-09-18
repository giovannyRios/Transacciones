using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Dominio.Context;
using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.MappingEntities
{
    public class MappingProfile:Profile
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
                    dest => dest.ClienteId,
                    opt => opt.MapFrom(src => src.ClienteId)
                )
                .ForMember(
                    dest => dest.PersonaId,
                    opt => opt.MapFrom(src => src.PersonaId)
                )
                .ForMember(
                    dest => dest.Contrasena,
                    opt => opt.MapFrom(src => src.Contrasena)
                ).ReverseMap();

            //Cuenta
            CreateMap<Cuentum, CuentaDTO>()
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
                ).ReverseMap();

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


            //Persona
            CreateMap<Persona, PersonaDTO>()
                .ForMember(
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
