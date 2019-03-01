using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportStore.Models;
using SportStore.Repo.Abstract;
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
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(p => p.Products).Returns(
                (
                    new Product[]
                     {
                         new Product {ProductID = 1, Name = "P1",Category = "Apples"},
                         new Product {ProductID = 2, Name = "P2",Category = "Apples"},
                         new Product {ProductID = 3, Name = "P3",Category = "Plums"},
                         new Product {ProductID = 4, Name = "P4",Category = "Oranges"}
                     }
                )
                .AsQueryable<Product>());

            var productService = new ProductService(productRepositoryMock.Object);

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

        [Fact]
        public async Task Indicate_Selected_Category()
        {
            string categorySelected = "Apples";

            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(p => p.Products).Returns(
                (
                    new Product[]
                     {
                         new Product {ProductID = 1, Name = "P1",Category = "Apples"},
                         new Product {ProductID = 2, Name = "P2",Category = "Oranges"},
                     }
                )
                .AsQueryable<Product>());

            var productService = new ProductService(productRepositoryMock.Object);

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