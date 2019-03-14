using SportStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportStore.Services.Abstract
{
    public interface ICardService
    {
        //Task CreateCard(int userId);
        
        Task AddItem(Product product, int quantity);

        Task RemoveItem(Product product);

        Task<decimal> ComputeTotalValue();

        Task Clear();

        Task<IEnumerable<CardItem>> GetAll();
    }
}