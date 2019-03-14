using SportStore.Models;
using SportStore.Services.DTOs;
using System.Threading.Tasks;

namespace SportStore.Services.Abstract
{
    public interface IProductService
    {
        Task<Product> GetProductById(int productId);
        Task<ProductsListDTO> GetAllProducts(string category, int productPage = 1);

        Task<CategoryesDTO> GetCategoryNames();
    }
}