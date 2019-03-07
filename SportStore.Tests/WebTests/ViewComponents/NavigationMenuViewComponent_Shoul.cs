using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Moq;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services;
using SportStore.Web.ViewComponents;
using SportStore.Web.ViewComponents.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Tests.WebTests.ViewComponents
{
    public class NavigationMenuViewComponent_Shoul
    {
        [Fact]
        public async Task Can_Select_Category_From_Navigation()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
           .UseInMemoryDatabase(databaseName: "Can_Select_Category_From_Navigation")
           .Options;

            using (var context = new SportStoreDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "P1", Category = "Apples" });
                context.Products.Add(new Product { Id = 2, Name = "P2", Category = "Apples" });
                context.Products.Add(new Product { Id = 3, Name = "P3", Category = "Plums" });
                context.Products.Add(new Product { Id = 4, Name = "P4", Category = "Oranges" });
              

                context.SaveChanges();

                var productService = new ProductService(context);

                NavigationMenuViewComponent target =
                               new NavigationMenuViewComponent(productService);

                var results = (
                    (CategoryViewModel)(await target.InvokeAsync() as ViewViewComponentResult)
                    .ViewData.Model);

                Assert.True(Enumerable
                        .SequenceEqual(
                            new string[] { "Apples", "Oranges", "Plums" }, results.Categoryes.ToArray()
                              )
                            );
            }
                     
        }

        [Fact]
        public async Task Indicate_Selected_Category()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
          .UseInMemoryDatabase(databaseName: "Indicate_Selected_Category")
          .Options;

            string categorySelected = "Apples";

            using (var context = new SportStoreDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "P1", Category = "Apples" });
                context.Products.Add(new Product { Id = 4, Name = "P2", Category = "Oranges" });


                context.SaveChanges();

                var productService = new ProductService(context);

                NavigationMenuViewComponent target =
               new NavigationMenuViewComponent(productService);

                target.ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
                    {
                        RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                    }
                };

                target.RouteData.Values["category"] = categorySelected;

                string result = (string)(await target.InvokeAsync() as ViewViewComponentResult)
                    .ViewData["SelectedCategory"];

                Assert.Equal(categorySelected, result);
            }
                     
        }
    }
}