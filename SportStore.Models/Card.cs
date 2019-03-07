using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Models
{
    public class Card : Entity
    {
        public Card()
        {
            this.CardLines = new HashSet<CardLine>();
        }     

        public ICollection<CardLine> CardLines { get; set; }
    }
}
