using SportStore.Services.DTOs;

namespace SportStore.Services.Abstract
{
    public interface IProductService
    {
        ProductsListDTO GetAllProducts(string category, int productPage = 1);

        CategoryesDTO GetCategoryNames();
    }
}