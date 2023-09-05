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
    public class SolicitudPermisoConfig : IEntityTypeConfiguration<SolicitudPermiso>
    {
        public void Configure(EntityTypeBuilder<SolicitudPermiso> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Fecha).IsRequired();
            builder.Property(p => p.Tipo).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Estado).IsRequired();
        }
    }
}
