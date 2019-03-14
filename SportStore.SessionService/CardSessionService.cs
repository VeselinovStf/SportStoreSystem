using Microsoft.AspNetCore.Http;
using SportStore.Models;
using SportStore.SessionService.Abstract;
using System;
using Newtonsoft.Json;

namespace SportStore.SessionService
{
    public class CardSessionService : ICardSessionService
    {
        private readonly IHttpContextAccessor httpContext;

        public CardSessionService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public void AddToCardSession(Product product)
        {

            if (product != null)
            {
                Cart cart = GetCard();
                cart.CardItems.Add(new CardItem() { Product = product });
                SaveCard(cart);
            }
        }

        public Cart GetCard()
        {
            Cart cart = this.httpContext.HttpContext
                .Session
                .GetJson<Cart>("Cart") ?? new Cart();
              
            return cart;
        }

        public void SaveCard(Cart cart)
        {
            this.httpContext
                .HttpContext
                .Session
                .SetJson("Cart", cart);
        }
    }
}
