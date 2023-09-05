using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SolicitudVacaciones> SolicitudesVacaciones { get; set; }
        public DbSet<SolicitudPermiso> SolicitudesPermisos { get; set; }
        public DbSet<InformeQueja> InformesQuejas { get; set; }
        public DbSet<Estadistica> Estadisticas { get; set; }
        public DbSet<ComunicacionInterna> ComunicacionesInternas { get; set; }
        
    }
}
