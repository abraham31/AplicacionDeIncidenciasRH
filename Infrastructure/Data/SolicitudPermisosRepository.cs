using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    internal class SolicitudPermisosRepository : Repository<SolicitudPermiso>, ISolicitudPermisoRepository
    {
        private readonly ApplicationDbContext _db;
        public SolicitudPermisosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<SolicitudPermiso> Actualizar(SolicitudPermiso Entity)
        {
            Entity.FechaDeModificacion = DateTime.Now;
            _db.SolicitudesPermisos.Update(Entity);
            await _db.SaveChangesAsync();
            return Entity;

        }
    }
}
