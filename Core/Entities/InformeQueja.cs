namespace Core.Entities
{
    public class InformeQueja : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime FechaInforme { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public EstadoIncidencia Estado { get; set; }
    }
}
