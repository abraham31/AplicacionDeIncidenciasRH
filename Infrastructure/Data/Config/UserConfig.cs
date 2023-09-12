using Core.Entities.Auth;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.UserName).IsRequired().IsUnicode();
            builder.Property(p => p.Names).IsRequired();
            builder.Property(p => p.Password).IsRequired();
        }
    }
}
