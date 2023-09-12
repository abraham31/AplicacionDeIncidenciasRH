namespace WEB_API.Dtos
{
    public class EstadisticaPorPeriodoDto
    {
        public string Periodo { get; set; }
        public int TotalSolicitudesVacaciones { get; set; }
        public int TotalSolicitudesPermisos { get; set; }
    }
}
