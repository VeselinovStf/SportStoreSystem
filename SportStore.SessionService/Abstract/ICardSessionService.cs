using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.SessionService.Abstract
{
    public interface ICardSessionService
    {
        void AddToCardSession(Product product);
        Cart GetCard();

        void SaveCard(Cart card);
    }
}
