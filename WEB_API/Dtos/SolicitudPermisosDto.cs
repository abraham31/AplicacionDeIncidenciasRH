using Core.Entities;

namespace WEB_API.Dtos
{
    public class SolicitudPermisosDto
    {
        public int UserId { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public EstadoIncidencia Estado { get; set; }
    }
}
