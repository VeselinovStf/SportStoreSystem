using Microsoft.EntityFrameworkCore;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Tests.CardServiceTests
{
    public class CalculateTotalValue_Should
    {
        [Fact]
        public async Task Calculate_Card_Total()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
              .UseInMemoryDatabase(databaseName: "Calculate_Card_Total")
              .Options;

            var cardItem1 = new CardItem()
            {
                Product = new Product() { Id = 1, Price = 5.0M, Name = "P1" },
                Quantity = 1
            };

            var cardItem2 = new CardItem()
            {
                Product = new Product() { Id = 2, Price = 5.0M, Name = "P2" },
                Quantity = 2
            };

            using (var context = new SportStoreDbContext(options))
            {
                await context.CardItems.AddAsync(cardItem1);
                await context.CardItems.AddAsync(cardItem2);

                await context.SaveChangesAsync();

                var service = new CardService(context);

                var resultSum = await service.ComputeTotalValue();

                Assert.Equal(15.0M, resultSum);
            }
        }
    }
}
