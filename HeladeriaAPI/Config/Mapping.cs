﻿using AutoMapper;
using HeladeriaAPI.Models;
using HeladeriaAPI.Models.Helado;
using HeladeriaAPI.Models.Helado.Dto;
using HeladeriaAPI.Models.Ingrediente;
using HeladeriaAPI.Models.Ingrediente.Dto;
using HeladeriaAPI.Models.Salsa;
using HeladeriaAPI.Models.Salsa.Dto;

namespace HeladeriaAPI.Config
{
    public class Mapping : Profile
    {
        public Mapping() {
            // Para no convertir los atributos 'int?' a 0 en la conversion de los 'null'
            // valor defecto int -> 0
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            // Para los precios utilizamos el mismo concepto.
            // valor defecto double -> 0
            CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);

            // Aqui es necesario hacer esto con bool? ya que tampoco devuelve el tipo 'null'.
            // valor defecto bool -> false
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);

            // Lo mismo con las listas.
            // valor defecto List -> []
            CreateMap<List<string>?, List<string>>().ConvertUsing((src, dest) => src ?? dest);

            //PD: Esta solución hay que aplicarla para todos aquellos tipos que no tengan como valor por defecto 'null'


            CreateMap<Helado, AllHeladoDTO>().ReverseMap();
            CreateMap<CreateHeladoDTO, Helado>().ReverseMap();
            

            // Actualizar y no parsear los valores 'NULL'
            // NO PONER 'ReverseMap()' ya que no queremos convertir de Helado a UpdateHeladoDTO y no va a funcionar la condición de los miembros.
            CreateMap<UpdateHeladoDTO, Helado>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            // Ingrediente mappings
            CreateMap<CreateIngredienteDTO, Ingrediente>().ReverseMap();
            CreateMap<UpdateIngredienteDTO, Ingrediente>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<CreateSalsaDTO, Salsa>().ForMember(dest => dest.Ingredientes, opt => opt.Ignore());
            CreateMap<UpdateSalsaDTO, Salsa>().ForAllMembers(opts =>
            {
                opts.Condition((src, dest, srcMember) => srcMember != null);
            });
            CreateMap<Salsa, AllSalsaDTO>().ForMember(dest =>
            dest.Ingredientes, opt => opt.MapFrom(src => src.Ingredientes.Select(i => i.Nombre)));

        }
    }
}
