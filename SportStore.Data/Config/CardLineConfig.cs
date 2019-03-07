using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Models;

namespace SportStore.Data.Config
{
    public class CardLineConfig : IEntityTypeConfiguration<CardLine>
    {
        public void Configure(EntityTypeBuilder<CardLine> builder)
        {
            builder.HasOne(c => c.Product)
                .WithOne(p => p.CardLine)
                .HasForeignKey<CardLine>(c => c.Id);
        }
    }
}