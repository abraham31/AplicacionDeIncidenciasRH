namespace Core.Entities
{
    public class SolicitudVacaciones : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public int? UsuarioAsignadoId { get; set; } 

    }
}
