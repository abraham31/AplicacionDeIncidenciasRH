using Core.Entities;

namespace WEB_API.Dtos
{
    public class SolicitudVacacionesDto
    {
        public int UserId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoIncidencia Estado { get; set; }
    }
}
