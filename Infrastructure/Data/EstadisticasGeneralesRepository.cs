using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    internal class EstadisticasGeneralesRepository : Repository<EstadisticasGenerales>, IEstadisticasGeneralesRepository
    {
        private readonly ApplicationDbContext _db;
        public EstadisticasGeneralesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
