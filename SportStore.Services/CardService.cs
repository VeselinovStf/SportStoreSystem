using Microsoft.EntityFrameworkCore;
using SportStore.Data;
using SportStore.Models;
using SportStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Services
{
    public class CardService : ICardService
    {
        private readonly SportStoreDbContext _cardRepository;

        public CardService(SportStoreDbContext cardRepository)
        {
            this._cardRepository = cardRepository;
        }

        public async Task AddItem(Product product, int quantity = 1)
        {
            //TODO: Main issue is that Based on my architecture I nead the card ID
            CardItem line = await this._cardRepository
                .CardItems
                .Include(p => p.Product)
                .Where(c => c.Id == product.Id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (line == null)
            {
                var newLine = new CardItem()
                {
                    Product = product,
                    Id = product.Id,
                    Quantity = quantity
                };

                this._cardRepository.Add(newLine);
                await this._cardRepository.SaveChangesAsync();
            }
            else
            {
                line.Quantity += quantity;
            }

           
        }

        public async Task Clear()
        {
            var lines = await this._cardRepository
                .CardItems
                .ToListAsync();

            foreach (var line in lines)
            {
                line.IsDeleted = true;
            }

            await this._cardRepository.SaveChangesAsync();
        }

        public async Task<decimal> ComputeTotalValue()
        {
            var cardLine = await this._cardRepository
                .CardItems
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            var totalValue = cardLine
                .Sum(p => p.Product.Price * p.Quantity);

            return totalValue;
        }

        public async Task<IEnumerable<CardItem>> GetAll()
        {
            return await this._cardRepository
                .CardItems
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task RemoveItem(Product product)
        {
            var item = await this._cardRepository
                .CardItems
                .FirstOrDefaultAsync(p => p.Id == product.Id && !p.IsDeleted);

            item.IsDeleted = true;

            await this._cardRepository.SaveChangesAsync();
          
        }
    }
}