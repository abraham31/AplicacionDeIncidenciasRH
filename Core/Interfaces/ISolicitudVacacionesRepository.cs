using Core.Entities;

namespace Core.Interfaces
{
    public interface ISolicitudVacacionesRepository : IRepository<SolicitudVacaciones>
    {
        Task<SolicitudVacaciones> Actualizar(SolicitudVacaciones Entity);
    }
}
