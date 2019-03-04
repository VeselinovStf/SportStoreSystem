using Microsoft.EntityFrameworkCore;
using SportStore.Data;
using SportStore.Models;
using SportStore.Repo.Abstract;
using System.Linq;

namespace SportStore.Repo
{
    public class EFCardRepository : ICardRepository
    {
        private readonly SportStoreDbContext context;

        public EFCardRepository(SportStoreDbContext context)
        {
            this.context = context;
        }

        public IQueryable<CardLine> CardLines => this.context.CardLine.Include(p => p.Product);

        public void AddCardLine(CardLine line)
        {
            this.context.CardLine.Add(line);

            this.context.SaveChanges();
        }

        public void Clear()
        {
            foreach (var line in this.context.CardLine)
            {
                line.IsDeleted = true;
            }

            this.context.SaveChanges();
        }

        public void RemoveLine(CardLine line)
        {
            this.context.CardLine.Remove(line);

            this.context.SaveChanges();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}