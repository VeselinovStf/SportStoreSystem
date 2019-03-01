using SportStore.Models;
using SportStore.Repo.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Repo
{
    public class FakeProductsRepository : IProductRepository
    {
        private readonly List<Product> FakeProducts =
            new List<Product>
                {
                    new Product { Name = "Football", Price = 25 },
                    new Product { Name = "Surf board", Price = 179 },
                    new Product { Name = "Running shoes", Price = 95 }
                };

        public IQueryable<Product> Products
        {
            get
            {
                return FakeProducts.AsQueryable<Product>();
            }
        }
    }
}