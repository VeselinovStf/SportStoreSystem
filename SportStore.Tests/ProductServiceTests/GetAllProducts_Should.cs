using Microsoft.EntityFrameworkCore;
using Moq;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Tests.ProductServiceTests
{
    public class GetAllProducts_Should
    {
        [Fact]
        public async Task Paginate_Correct()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
              .UseInMemoryDatabase(databaseName: "Paginate_Correct")
              .Options;

            using (var context = new SportStoreDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "P1" });
                context.Products.Add(new Product { Id = 2, Name = "P2" });
                context.Products.Add(new Product { Id = 3, Name = "P3" });
                context.Products.Add(new Product { Id = 4, Name = "P4" });
                context.Products.Add(new Product { Id = 5, Name = "P5" });

                context.SaveChanges();

                var productService = new ProductService(context);

                productService.PageSize = 3;

                var productsQuery = await productService.GetAllProducts(null, 2);
                 var products =    productsQuery.Products.ToArray();

                Assert.True(products.Length == 2);
                Assert.Equal("P4", products[0].Name);
                Assert.Equal("P5", products[1].Name);
            }
        }

        [Fact]
        public async Task Can_Send_Pagination_View_Model()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
              .UseInMemoryDatabase(databaseName: "Can_Send_Pagination_View_Model")
              .Options;

            using (var context = new SportStoreDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "P1" });
                context.Products.Add(new Product { Id = 2, Name = "P2" });
                context.Products.Add(new Product { Id = 3, Name = "P3" });
                context.Products.Add(new Product { Id = 4, Name = "P4" });
                context.Products.Add(new Product { Id = 5, Name = "P5" });

                context.SaveChanges();

                var productService = new ProductService(context);

                productService.PageSize = 3;

                var resultModel = await productService.GetAllProducts(null, 2);

                var pageInfo = resultModel.PagingInfo;

                Assert.Equal(2, pageInfo.CurrentPage);
                Assert.Equal(3, pageInfo.ItemsPerPage);
                Assert.Equal(5, pageInfo.TotalItems);
                Assert.Equal(2, pageInfo.TotalPages);

            }
        }

        [Fact]
        public async Task Can_By_Category()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
             .UseInMemoryDatabase(databaseName: "Can_By_Category")
             .Options;

            using (var context = new SportStoreDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Name = "P1", Category = "Cat1" });
                context.Products.Add(new Product { Id = 2, Name = "P2", Category = "Cat2" });
                context.Products.Add(new Product { Id = 3, Name = "P3", Category = "Cat3" });
                context.Products.Add(new Product { Id = 4, Name = "P4", Category = "Cat2" });
                context.Products.Add(new Product { Id = 5, Name = "P5", Category = "Cat5" });

                context.SaveChanges();

                var productService = new ProductService(context);

                productService.PageSize = 3;
                var resultModelQuery = await productService.GetAllProducts("Cat2", 1);
                var resultModel = resultModelQuery.Products
                    .ToArray();

                Assert.Equal(2, resultModel.Length);
                Assert.True(resultModel[0].Name == "P2" && resultModel[0].Category == "Cat2");
                Assert.True(resultModel[1].Name == "P4" && resultModel[1].Category == "Cat2");
            }
        }

        [Fact]
        public async Task Generate_Category_Specific_Product_Count()
        {
            var options = new DbContextOptionsBuilder<SportStoreDbContext>()
            .UseInMemoryDatabase(databaseName: "Generate_Category_Specific_Product_Count")
            .Options;

            using (var context = new SportStoreDbContext(options))
            {
                context.Products.Add(new Product {Id = 1, Name = "P1", Category = "Cat1"});
                context.Products.Add(new Product {Id = 2, Name = "P2", Category = "Cat2"});
                context.Products.Add(new Product {Id = 3, Name = "P3", Category = "Cat1"});
                context.Products.Add(new Product {Id = 4, Name = "P4", Category = "Cat2"});
                context.Products.Add(new Product { Id = 5, Name = "P5", Category = "Cat3" });

                context.SaveChanges();

                var productService = new ProductService(context);

         
                productService.PageSize = 5;

                var res1Query = await productService.GetAllProducts("Cat1");
                var res2Query = await productService.GetAllProducts("Cat2");
                var res3Query = await productService.GetAllProducts("Cat3");
                var resAllQuery = await productService.GetAllProducts(null);

                int? res1 = res1Query.Products.Count();
                int? res2 = res2Query.Products.Count();
                int? res3 = res3Query.Products.Count();
                int? resAll = resAllQuery.Products.Count();

                Assert.Equal(2, res1);
                Assert.Equal(2, res2);
                Assert.Equal(1, res3);
                Assert.Equal(5, resAll);
            }
           
        }
    }
}