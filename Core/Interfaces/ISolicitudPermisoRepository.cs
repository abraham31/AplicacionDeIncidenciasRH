using Core.Entities;

namespace Core.Interfaces
{
    public interface ISolicitudPermisoRepository :IRepository<SolicitudPermiso>
    {
        Task<SolicitudPermiso> Actualizar(SolicitudPermiso Entity);
    }
}
