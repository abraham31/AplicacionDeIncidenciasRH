namespace Core.Entities
{
    public class SolicitudPermiso : BaseEntity
    {
        public string UserId { get; set; }
        public DateTime FechaPermiso { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public EstadoIncidencia Estado { get; set; }
    }
}
