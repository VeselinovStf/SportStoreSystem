using Microsoft.AspNetCore.Mvc;
using SportStore.Services.Abstract;
using SportStore.Web.ViewComponents.ViewModels;
using System.Threading.Tasks;

namespace SportStore.Web.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public NavigationMenuViewComponent(IProductService productService)
        {
            this._productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryes = this._productService.GetCategoryNames();

            var model = new CategoryViewModel(categoryes);
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            //TODO: Fix This Issue!!!
            //model.SelectedCategory = RouteData?.Values["category"].ToString();

            return View("NavigationMenu", model);
        }
    }
}