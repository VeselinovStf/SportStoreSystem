using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Data.Config
{
    public class CardConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasMany(c => c.CardItems)
                .WithOne(cl => cl.Card)
                .HasForeignKey(cl => cl.Card_Id);
        }
    }
}
