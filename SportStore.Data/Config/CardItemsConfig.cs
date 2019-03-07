using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Models;

namespace SportStore.Data.Config
{
    public class CardItemsConfig : IEntityTypeConfiguration<CardItem>
    {
        public void Configure(EntityTypeBuilder<CardItem> builder)
        {
            builder.HasOne(c => c.Product)
                .WithOne(p => p.CardLine)
                .HasForeignKey<CardItem>(c => c.Id);

            builder.Property(p => p.Quantity)
                .HasColumnType("int")             
                .IsRequired();
        }
    }
}