using SportStore.Models;
using System.Linq;

namespace SportStore.Repo.Abstract
{
    public interface ICardListRepository
    {
        IQueryable<CardLine> CardLines { get; }

        void AddCardLine(CardLine line);

        void RemoveLine(CardLine id);

        void Clear();

        void SaveChanges();
    }
}