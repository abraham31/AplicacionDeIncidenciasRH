﻿using AutoMapper;
using Core.Entities;
using WEB_API.Dtos;

namespace WEB_API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SolicitudVacaciones, SolicitudVacacionesDto>().ReverseMap();
            CreateMap<SolicitudPermiso, SolicitudPermisosDto>().ReverseMap();
            CreateMap<InformeQueja, InformeQuejaDto>().ReverseMap();
            CreateMap<Estadistica, EstadisticaDto>().ReverseMap();
            CreateMap<ComunicacionInterna, ComunicacionInternaDto>().ReverseMap();
        }
    }
}
