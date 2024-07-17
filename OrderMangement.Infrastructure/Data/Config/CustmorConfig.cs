using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangement.Infrastructure.Data.Config
{
    public class CustmorConfig : IEntityTypeConfiguration<Custmor>
    {
        public void Configure(EntityTypeBuilder<Custmor> builder)
        {
            builder.HasMany(C => C.Orders)
                .WithOne();
            builder.Property(C => C.Name).IsRequired().HasMaxLength(50);
            builder.Property(C =>C.Email).IsRequired();
        }
    }
}
