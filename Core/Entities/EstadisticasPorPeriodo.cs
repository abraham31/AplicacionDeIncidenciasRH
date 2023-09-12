namespace Core.Entities
{
    public class EstadisticasPorPeriodo : BaseEntity
    {
        public string Periodo { get; set; } 
        public int TotalSolicitudesVacaciones { get; set; }
        public int TotalSolicitudesPermisos { get; set; }
        public int TotalInformseQuejas { get; set; }
    }
}
