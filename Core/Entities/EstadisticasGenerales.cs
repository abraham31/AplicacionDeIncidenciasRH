namespace Core.Entities
{
    public class EstadisticasGenerales : BaseEntity
    {
        public int TotalSolicitudesVacaciones { get; set; }
        public int TotalSolicitudesPermisos { get; set; }
        public int TotalInformseQuejas { get; set; }
    }
}
