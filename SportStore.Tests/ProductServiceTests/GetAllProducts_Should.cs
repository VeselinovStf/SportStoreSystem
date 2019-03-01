using Moq;
using SportStore.Models;
using SportStore.Repo.Abstract;
using SportStore.Services;
using System.Linq;
using Xunit;

namespace SportStore.Tests.ProductServiceTests
{
    public class GetAllProducts_Should
    {
        [Fact]
        public void Paginate_Correct()
        {
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(p => p.Products).Returns(
                (
                    new Product[]
                     {
                         new Product {ProductID = 1, Name = "P1"},
                         new Product {ProductID = 2, Name = "P2"},
                         new Product {ProductID = 3, Name = "P3"},
                         new Product {ProductID = 4, Name = "P4"},
                         new Product {ProductID = 5, Name = "P5"}
                     }
                )
                .AsQueryable<Product>());

            var productService = new ProductService(productRepositoryMock.Object);

            productService.PageSize = 3;

            var products = productService.GetAllProducts(null, 2)
                .Products.ToArray();

            Assert.True(products.Length == 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(p => p.Products).Returns(
                (
                    new Product[]
                     {
                         new Product {ProductID = 1, Name = "P1"},
                         new Product {ProductID = 2, Name = "P2"},
                         new Product {ProductID = 3, Name = "P3"},
                         new Product {ProductID = 4, Name = "P4"},
                         new Product {ProductID = 5, Name = "P5"}
                     }
                )
                .AsQueryable<Product>());

            var productService = new ProductService(productRepositoryMock.Object);

            productService.PageSize = 3;

            var resultModel = productService.GetAllProducts(null, 2);

            var pageInfo = resultModel.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_By_Category()
        {
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(p => p.Products).Returns(
                (
                    new Product[]
                     {
                         new Product {ProductID = 1, Name = "P1", Category="Cat1"},
                         new Product {ProductID = 2, Name = "P2", Category="Cat2"},
                         new Product {ProductID = 3, Name = "P3", Category="Cat3"},
                         new Product {ProductID = 4, Name = "P4", Category="Cat2"},
                         new Product {ProductID = 5, Name = "P5", Category="Cat5"}
                     }
                )
                .AsQueryable<Product>());

            var productService = new ProductService(productRepositoryMock.Object);

            productService.PageSize = 3;

            var resultModel = productService.GetAllProducts("Cat2", 1)
                .Products
                .ToArray();

            Assert.Equal(2, resultModel.Length);
            Assert.True(resultModel[0].Name == "P2" && resultModel[0].Category == "Cat2");
            Assert.True(resultModel[1].Name == "P4" && resultModel[1].Category == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(p => p.Products).Returns(
                (
                    new Product[]
                     {
                         new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                         new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                         new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                         new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                         new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
                     }
                )
                .AsQueryable<Product>());

            var productService = new ProductService(productRepositoryMock.Object);

            productService.PageSize = 5;

            int? res1 = productService.GetAllProducts("Cat1").Products.Count();
            int? res2 = productService.GetAllProducts("Cat2").Products.Count();
            int? res3 = productService.GetAllProducts("Cat3").Products.Count();
            int? resAll = productService.GetAllProducts(null).Products.Count();

            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}