using Microsoft.AspNetCore.Mvc;
using SportStore.Services.Abstract;
using SportStore.SessionService.Abstract;
using SportStore.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICardService _cardRepository;
        private readonly IProductService _productRepository;
        private readonly ICardSessionService _cardSessionService;

        public CartController(
            ICardService cardRepository, 
            IProductService productRepository,
            ICardSessionService cardSessionService)
        {
            this._cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this._productRepository = productRepository ?? throw new ArgumentNullException(nameof(_productRepository));
            this._cardSessionService = cardSessionService ?? throw new ArgumentNullException(nameof(_cardSessionService));
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = this._cardSessionService.GetCard(),
                ReturnUrl = returnUrl
            });
        }

        public async Task<RedirectToActionResult> AddToCart(int productId, string returnUrl)
        {
            var product = await this._productRepository.GetProductById(productId);

            
            this._cardSessionService.AddToCardSession(product);

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
