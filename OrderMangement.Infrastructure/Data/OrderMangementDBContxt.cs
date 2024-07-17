using Microsoft.EntityFrameworkCore;
using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderMangement.Infrastructure.Data
{
    public class OrderMangementDBContxt : DbContext
    {
        public OrderMangementDBContxt(DbContextOptions<OrderMangementDBContxt> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Custmor> Custmors { get; set; }
        public Invoice Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
