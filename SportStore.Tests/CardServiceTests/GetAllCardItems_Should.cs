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

    public class GetAllCardItems_Should
    {
        [Fact]
        public async Task Get_All_Card_Items()
        {

            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
                    .UseInMemoryDatabase(databaseName: "Get_All_Card_Items")
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

                var result = await service.GetAll();

                var resultCount = result.ToList().Count;

                var savedItems = await context
                    .CardItems
                    .ToListAsync();

                Assert.Equal(savedItems.Count, resultCount);
            }
        }
    }
}
