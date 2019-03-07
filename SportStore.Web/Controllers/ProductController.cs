using Microsoft.AspNetCore.Mvc;
using SportStore.Services.Abstract;
using SportStore.Web.ViewModels;
using System.Threading.Tasks;

namespace SportStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        public async Task<ViewResult> List(string category, int productPage = 1)
        {
            var serviceModel = await this._productService.GetAllProducts(category, productPage);

            var model = new ProductsListViewModel(serviceModel);

            return View(model);
        }
    }
}