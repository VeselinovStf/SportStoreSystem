using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Data.Config
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasMany(c => c.CardLines)
                .WithOne(cl => cl.Card)
                .HasForeignKey(cl => cl.Card_Id);
        }
    }
}
