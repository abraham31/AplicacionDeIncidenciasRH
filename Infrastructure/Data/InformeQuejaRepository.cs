using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    internal class InformeQuejaRepository : Repository<InformeQueja>, IInformeQuejaRepository
    {
        private readonly ApplicationDbContext _db;
        public InformeQuejaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<InformeQueja> Actualizar(InformeQueja Entity)
        {
            Entity.FechaDeModificacion = DateTime.Now;
            _db.InformesQuejas.Update(Entity);
            await _db.SaveChangesAsync();
            return Entity;
        }
    }
}
