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

        public async Task AddItem(Product product, int quantity)
        {
            //TODO: Main issue is that Based on my architecture I nead the card ID
            CardLine line = await this._cardRepository
                .CardLine
                .Include(p => p.Product)
                .Where(c => c.Id == product.Id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (line == null)
            {
                var newLine = new CardLine()
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
                .CardLine
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
                .CardLine
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            var totalValue = cardLine
                .Sum(p => p.Product.Price * p.Quantity);

            return totalValue;
        }

        public async Task<IEnumerable<CardLine>> GetAll()
        {
            return await this._cardRepository
                .CardLine
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task RemoveLine(Product product)
        {
            var item = await this._cardRepository
                .CardLine
                .FirstOrDefaultAsync(p => p.Id == product.Id && !p.IsDeleted);

            item.IsDeleted = true;

            await this._cardRepository.SaveChangesAsync();
          
        }
    }
}