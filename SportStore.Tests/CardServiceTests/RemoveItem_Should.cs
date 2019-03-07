using Microsoft.EntityFrameworkCore;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Tests.CardServiceTests
{
    public class RemoveItem_Should
    {
        [Fact]
        public async Task Can_Remove_Line()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
               .UseInMemoryDatabase(databaseName: "Can_Remove_Line")
               .Options;

            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            using (var context = new SportStoreDbContext(options))
            {
                var newLine1 = new CardItem()
                {
                    Product = p1,
                    Id = p1.Id,
                    Quantity = 1
                };

                var newLine2 = new CardItem()
                {
                    Product = p2,
                    Id = p2.Id,
                    Quantity = 2
                };

                context.CardItems.Add(newLine1);
                context.CardItems.Add(newLine2);

                await context.SaveChangesAsync();

                var service = new CardService(context);

                await service.RemoveItem(p2);

                var result = context.CardItems.ToArray();

                Assert.Single(result.Where(c => c.Product == p2 && c.IsDeleted));
                Assert.Single(result.Where(c => c.Product == p1 && !c.IsDeleted));
            }
        }
    }
}
