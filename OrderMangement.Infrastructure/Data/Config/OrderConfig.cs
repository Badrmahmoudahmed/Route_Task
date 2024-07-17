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
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(O => O.OrderItems)
                .WithOne();
                
            
            builder.Property(O => O.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (Statues)Enum.Parse(typeof(Statues), o));

            builder.Property(O => O.PaymentMethod)
                .HasConversion(
                    o => o.ToString(),
                    o => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), o)
                    );

            builder.Property(O => O.TotalAmount).HasColumnType("decimal(18,2)");


        }
    }
}
