using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(p => p.Category)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("money")
                .IsRequired();
        }
    }
}
