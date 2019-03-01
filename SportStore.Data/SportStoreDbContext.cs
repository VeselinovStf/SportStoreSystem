using Microsoft.EntityFrameworkCore;
using SportStore.Data.Config;
using SportStore.Models;

namespace SportStore.Data
{
    public class SportStoreDbContext : DbContext
    {
        public SportStoreDbContext(DbContextOptions<SportStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CardLine> CardLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardLineConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}