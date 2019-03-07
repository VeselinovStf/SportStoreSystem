using SportStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Services.Abstract
{
    public interface ICardService
    {
        Task AddItem(Product product, int quantity);

        Task RemoveLine(Product product);

        Task<decimal> ComputeTotalValue();

        Task Clear();

        Task<IEnumerable<CardLine>> GetAll();
    }
}