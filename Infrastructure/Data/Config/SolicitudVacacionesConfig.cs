using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class SolicitudVacacionesConfig : IEntityTypeConfiguration<SolicitudVacaciones>
    {
        public void Configure(EntityTypeBuilder<SolicitudVacaciones> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.FechaInicio).IsRequired();
            builder.Property(p => p.FechaFin).IsRequired();
            builder.Property(p => p.Estado).IsRequired();
        }
    }
}
