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
   public class Cleare_Should
    {
        [Fact]
        public async Task Can_Cleare_Content()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "Can_Cleare_Content")
                .Options;

            var cardItem1 = new CardItem()
            {
                Product = new Product() { Id = 1, Price = 5.0M, Name = "P1" },
                Quantity = 1,
                IsDeleted = false
            };

            var cardItem2 = new CardItem()
            {
                Product = new Product() { Id = 2, Price = 5.0M, Name = "P2" },
                Quantity = 2,
                IsDeleted = false
            };

            var cardItem3 = new CardItem()
            {
                Product = new Product() { Id = 3, Price = 5.0M, Name = "P2" },
                Quantity = 2,
                IsDeleted = false
            };

            using (var context = new SportStoreDbContext(options))
            {
                await context.CardItems.AddAsync(cardItem1);
                await context.CardItems.AddAsync(cardItem2);
                await context.CardItems.AddAsync(cardItem3);

                await context.SaveChangesAsync();

                var service = new CardService(context);

                await service.Clear();

                var cleared = await context
                    .CardItems
                    .ToListAsync();

                var clearedCount = cleared.Where(c => c.IsDeleted).Count();

                Assert.Equal(3, clearedCount);
            }
        }
    }
}
