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
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasOne(I => I.order)
                .WithOne()
                .HasForeignKey<Invoice>(I => I.OrderId);
            builder.Property(I => I.TotalAmount).HasColumnType("decimal(18,2)");
        }
    }
}
