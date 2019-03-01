using SportStore.Data;
using SportStore.Models;
using SportStore.Repo.Abstract;
using System.Linq;

namespace SportStore.Repo
{
    public class EFCardLineRepository : ICardListRepository
    {
        private readonly SportStoreDbContext context;

        public EFCardLineRepository(SportStoreDbContext context)
        {
            this.context = context;
        }

        public IQueryable<CardLine> CardLines => this.context.CardLine;

        public void AddCardLine(CardLine line)
        {
            this.context.CardLine.Add(line);
        }

        public void Clear()
        {
            foreach (var line in this.context.CardLine)
            {
                line.IsDeleted = true;
            }
        }

        public void RemoveLine(CardLine line)
        {
            this.context.CardLine.Remove(line);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}