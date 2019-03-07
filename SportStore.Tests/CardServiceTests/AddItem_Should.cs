﻿using Microsoft.EntityFrameworkCore;
using Moq;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services;
using SportStore.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Tests.CardServiceTests
{
   public class AddItem_Should
    {
        [Fact]
        public async Task Can_Add_New_Lines()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "Can_Add_New_Lines")
                .Options;

            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };


            using (var context = new SportStoreDbContext(options))
            {
               

                var service = new CardService(context);

                await service.AddItem(p1, 1);
                await service.AddItem(p2, 1);

                var result = context.CardLine.ToArray();

                Assert.Equal(2, result.Length);
                Assert.Equal(p1, result[0].Product);
                Assert.Equal(p2, result[1].Product);
            }
           
        }

        [Fact]
        public async Task Can_Add_Quantity_For_Existing_Lines()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
               .UseInMemoryDatabase(databaseName: "Can_Add_Quantity_For_Existing_Lines")
               .Options;

            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };



            using (var context = new SportStoreDbContext(options))
            {
               

                var service = new CardService(context);

               await service.AddItem(p1, 1);
               await service.AddItem(p2, 1);
               await service.AddItem(p1, 10);

                var result = context.CardLine.ToArray();

                Assert.Equal(2, result.Length);
                Assert.Equal(11, result[0].Quantity);
                Assert.Equal(1, result[1].Quantity);
            }
        }

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
               

                var newLine1 = new CardLine()
                {
                    Product = p1,
                    Id = p1.Id,
                    Quantity = 1
                };

                var newLine2 = new CardLine()
                {
                    Product = p2,
                    Id = p2.Id,
                    Quantity = 2
                };

                context.CardLine.Add(newLine1);
                context.CardLine.Add(newLine2);

               await  context.SaveChangesAsync();

                var service = new CardService(context);

                await service.RemoveLine(p2);

                var result = context.CardLine.ToArray();

                Assert.Single(result.Where(c => c.Product == p2 && c.IsDeleted));
                Assert.Single(result.Where(c => c.Product == p1 && !c.IsDeleted));
            }
        }
    }
}
