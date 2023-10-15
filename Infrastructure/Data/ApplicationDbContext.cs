using Core.Entities;
using Core.Entities.Auth;
using Infrastructure.Data.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserApplication>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<SolicitudVacaciones> SolicitudesVacaciones { get; set; }
        public DbSet<SolicitudPermiso> SolicitudesPermisos { get; set; }
        public DbSet<InformeQueja> InformesQuejas { get; set; }
        public DbSet<EstadisticasPorPeriodo> EstadisticaPorPeriodo { get; set; }
        public DbSet<EstadisticasGenerales> EstadisticasGenerales { get; set; }
        public DbSet<UserApplication> UserApplication { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new InformeQuejaConfig());
            builder.ApplyConfiguration(new SolicitudPermisoConfig());
            builder.ApplyConfiguration(new SolicitudVacacionesConfig());
        }

    }
}
