using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class SolicitudVacacionesRepository : Repository<SolicitudVacaciones>, ISolicitudVacacionesRepository
    {
        private readonly ApplicationDbContext _db;

        public SolicitudVacacionesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<SolicitudVacaciones> Actualizar(SolicitudVacaciones Entity)
        {
            Entity.FechaDeModificacion = DateTime.Now;
            _db.SolicitudesVacaciones.Update(Entity);
            await _db.SaveChangesAsync();
            return Entity;
        }
    }
}
