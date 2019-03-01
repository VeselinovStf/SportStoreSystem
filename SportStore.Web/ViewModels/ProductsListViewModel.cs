using SportStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Web.ViewModels
{
    public class ProductsListViewModel
    {
        public ProductsListViewModel(ProductsListDTO serviceDto)
        {
            this.Products = serviceDto.Products.Select(dto => new ProductViewModel()
            {
                Category = dto.Category,
                Description = dto.Description,
                Name = dto.Name,
                Price = dto.Price,
                ProductID = dto.ProductID
            });

            this.PagingInfo = new PagingInfo()
            {
                CurrentPage = serviceDto.PagingInfo.CurrentPage,
                ItemsPerPage = serviceDto.PagingInfo.ItemsPerPage,
                TotalItems = serviceDto.PagingInfo.TotalItems
            };

            this.CurrentCategory = serviceDto.CurrentCategory;
        }

        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}