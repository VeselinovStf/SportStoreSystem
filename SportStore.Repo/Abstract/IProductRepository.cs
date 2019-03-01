using SportStore.Models;
using System.Linq;

namespace SportStore.Repo.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}