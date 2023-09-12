using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    internal class InformeQuejaConfig : IEntityTypeConfiguration<InformeQueja>
    {
        public void Configure(EntityTypeBuilder<InformeQueja> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Titulo).IsRequired();
            builder.Property(p => p.Descripcion).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Estado).IsRequired();
            
        }
    }
}
