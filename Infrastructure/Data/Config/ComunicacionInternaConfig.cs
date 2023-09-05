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
    internal class ComunicacionInternaConfig : IEntityTypeConfiguration<ComunicacionInterna>
    {
        public void Configure(EntityTypeBuilder<ComunicacionInterna> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Mensaje).IsRequired();
            builder.Property(p => p.FechaEnvio).IsRequired();
        }
    }
}
