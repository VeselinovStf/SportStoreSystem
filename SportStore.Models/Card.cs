using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Models
{
    public class Card
    {
        public Card()
        {
            this.CardLines = new HashSet<CardLine>();
        }

        public int CardId { get; set; }

        public ICollection<CardLine> CardLines { get; set; }
    }
}
