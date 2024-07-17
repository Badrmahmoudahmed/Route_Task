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
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(OI => OI.Product)
                .WithMany()
                .HasForeignKey(OI => OI.ProductId);
            builder.Property(OI => OI.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(OI => OI.Discount).HasColumnType("decimal(18,2)");
            
        }
    }
}
