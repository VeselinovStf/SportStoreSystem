using SportStore.Models;
using System.Collections.Generic;

namespace SportStore.Services.Abstract
{
    public interface ICardService
    {
        void AddItem(Product product, int quantity);

        void RemoveLine(Product product);

        decimal ComputeTotalValue();

        void Clear();

        IEnumerable<CardLine> GetAll();
    }
}