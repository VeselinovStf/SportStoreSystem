using SportStore.Services.DTOs;
using System.Threading.Tasks;

namespace SportStore.Services.Abstract
{
    public interface IProductService
    {
        Task<ProductsListDTO> GetAllProducts(string category, int productPage = 1);

        Task<CategoryesDTO> GetCategoryNames();
    }
}