namespace WEB_API.Dtos
{
    public class EstadisticaDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalSolicitudes { get; set; }
        public int TotalInformesQuejas { get; set; }
        public int TotalComunicaciones { get; set; }
        public int TotalResueltas { get; set; }
        public int TotalPendientes { get; set; }
    }
}
