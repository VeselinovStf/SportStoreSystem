using SportStore.Models;
using SportStore.Repo.Abstract;
using SportStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            this._cardRepository = cardRepository;
        }

        public void AddItem(Product product, int quantity)
        {
            //TODO: Main issue is that Based on my architecture I nead the card ID
            CardLine line = this._cardRepository
                .CardLines
                .Where(c => c.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                var newLine = new CardLine()
                {
                    Product = product,
                    ProductID = product.ProductID,
                    Quantity = quantity
                };

                this._cardRepository.AddCardLine(newLine);
            }
            else
            {
                line.Quantity += quantity;
            }

           
        }

        public void Clear()
        {
            this._cardRepository.Clear();
        }

        public decimal ComputeTotalValue()
        {
            var cardLine = this._cardRepository.CardLines;

            var totalValue = cardLine.Sum(p => p.Product.Price * p.Quantity);

            return totalValue;
        }

        public IEnumerable<CardLine> GetAll()
        {
            return this._cardRepository.CardLines;
        }

        public void RemoveLine(Product product)
        {
            var item = this._cardRepository
                .CardLines
                .FirstOrDefault(p => p.ProductID == product.ProductID);

            this._cardRepository.RemoveLine(item);
          
        }
    }
}