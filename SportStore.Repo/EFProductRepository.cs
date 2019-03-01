using SportStore.Data;
using SportStore.Models;
using SportStore.Repo.Abstract;
using System.Linq;

namespace SportStore.Repo
{
    public class EFProductRepository : IProductRepository
    {
        private readonly SportStoreDbContext _context;

        public EFProductRepository(SportStoreDbContext ctx)
        {
            this._context = ctx;
        }

        public IQueryable<Product> Products => this._context.Products;
    }
}