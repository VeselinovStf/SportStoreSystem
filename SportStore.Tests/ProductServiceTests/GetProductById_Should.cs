using Microsoft.EntityFrameworkCore;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Tests.ProductServiceTests
{
    public class GetProductById_Should
    {
        [Fact]
        public async Task Return_Product_ById()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "Return_Product_ById")
                .Options;

            var productId = 1;
            var productName = "P1";

            var product = new Product()
            {
                Id = productId,
                Name = productName
            };

            using (var context = new SportStoreDbContext(options))
            {
                await context.Products.AddAsync(product);

                await context.SaveChangesAsync();

                var service = new ProductService(context);
                var result = await service.GetProductById(productId);

                Assert.Equal(productId, result.Id);
                Assert.Equal(productName, result.Name);
            }
        }
    }
}
