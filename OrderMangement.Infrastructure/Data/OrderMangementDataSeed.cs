using OrederManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderMangement.Infrastructure.Data
{
    public static class OrderMangementDataSeed
    {
        public static async Task SeedAsync(OrderMangementDBContxt dbcontext)
        {
            if (!dbcontext.Products.Any())
            {
                var ProductData = File.ReadAllText("../OrderMangement.Infrastructure/Data/DataSeed/Product.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                foreach (var item in products)
                {
                    await dbcontext.Set<Product>().AddAsync(item);
                }
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}
