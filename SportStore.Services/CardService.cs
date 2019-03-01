using SportStore.Models;
using SportStore.Repo.Abstract;
using SportStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Services
{
    public class CardService : ICardService
    {
        private readonly ICardListRepository _cardRepository;

        public CardService(ICardListRepository cardRepository)
        {
            this._cardRepository = cardRepository;
        }

        public void AddItem(Product product, int quantity)
        {
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

            this._cardRepository.SaveChanges();
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
            this._cardRepository.SaveChanges();
        }
    }
}