using SportStore.Repo.Abstract;
using SportStore.Services.Abstract;
using SportStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public int PageSize = 4;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductsListDTO GetAllProducts(string category, int productPage = 1)
        {
            var repoQuery =
                this._productRepository
                .Products
                .Where(c => category == null || c.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var serviceModel = new ProductsListDTO()
            {
                Products = repoQuery.Select(q => new ProductDTO()
                {
                    Category = q.Category,
                    Description = q.Description,
                    Name = q.Name,
                    Price = q.Price,
                    ProductID = q.ProductID
                }),
                PagingInfo = new PagingInfoDTO()
                {
                    TotalItems = category == null ?
                        this._productRepository
                        .Products
                        .Count()
                        :
                        this._productRepository
                        .Products
                        .Where(pc => pc.Category == category)
                        .Count(),
                    CurrentPage = productPage,
                    ItemsPerPage = this.PageSize
                },
                CurrentCategory = category
            };

            return serviceModel;
        }

        public CategoryesDTO GetCategoryNames()
        {
            var repoQuery = this._productRepository
                .Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p)
                .ToList();

            var serviceModel = new CategoryesDTO()
            {
                Categoryes = repoQuery.Select(c => c)
            };

            return serviceModel;
        }
    }
}