using Microsoft.AspNetCore.Mvc;
using SportStore.Services.Abstract;
using SportStore.Web.ViewModels;

namespace SportStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        public ViewResult List(string category, int productPage = 1)
        {
            var serviceModel = this._productService.GetAllProducts(category, productPage);

            var model = new ProductsListViewModel(serviceModel);

            return View(model);
        }
    }
}