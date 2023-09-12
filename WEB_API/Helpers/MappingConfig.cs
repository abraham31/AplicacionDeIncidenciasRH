using AutoMapper;
using Core.Entities;
using Core.Entities.Auth;
using Core.Entities.Auth.AuthDto;
using WEB_API.Dtos;

namespace WEB_API.Helpers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<SolicitudVacaciones, SolicitudVacacionesDto>().ReverseMap();
            CreateMap<SolicitudPermiso, SolicitudPermisosDto>().ReverseMap();
            CreateMap<InformeQueja, InformeQuejaDto>().ReverseMap();
            CreateMap<EstadisticasGenerales, EstadisticasGeneralesDto>().ReverseMap();
            CreateMap<EstadisticasPorPeriodo, EstadisticasGeneralesDto>().ReverseMap();
            CreateMap<UserApplication, UserDto>().ReverseMap();

        }
    }
}
