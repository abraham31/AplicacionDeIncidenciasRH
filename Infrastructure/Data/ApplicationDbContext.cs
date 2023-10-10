using Core.Entities;
using Core.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PromoProductConfiguration());
        }

    }
}
