using Microsoft.EntityFrameworkCore;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services.Abstract;
using SportStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Services
{
    public class ProductService : IProductService
    {
        private readonly SportStoreDbContext _productRepository;

        public int PageSize = 4;

        public ProductService(SportStoreDbContext productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductsListDTO> GetAllProducts(string category, int productPage = 1)
        {
            var repoQuery =
                await this._productRepository
                .Products
                .Where(c => category == null || c.Category == category)
                .OrderBy(p => p.Id)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            var serviceModel = new ProductsListDTO()
            {
                Products = repoQuery.Select(q => new ProductDTO()
                {
                    Category = q.Category,
                    Description = q.Description,
                    Name = q.Name,
                    Price = q.Price,
                    Id = q.Id
                }),
                PagingInfo = new PagingInfoDTO()
                {
                    TotalItems = category == null ?
                        await this._productRepository
                        .Products
                        .CountAsync()
                        :
                        await this._productRepository
                        .Products
                        .Where(pc => pc.Category == category)
                        .CountAsync(),
                    CurrentPage = productPage,
                    ItemsPerPage = this.PageSize
                },
                CurrentCategory = category
            };

            return serviceModel;
        }

        public async Task<CategoryesDTO> GetCategoryNames()
        {
            var repoQuery = await this._productRepository
                .Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p)
                .ToListAsync();

            var serviceModel = new CategoryesDTO()
            {
                Categoryes = repoQuery.Select(c => c)
            };

            return serviceModel;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var repoQuery = await this._productRepository
                .Products
                .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);

            //var serviceDto = new ProductDTO()
            //{
            //    Id = repoQuery.Id,
            //    Category = repoQuery.Category,
            //    Description = repoQuery.Description,
            //    Name = repoQuery.Name,
            //    Price = repoQuery.Price
            //};

            return repoQuery;
        }
    }
}