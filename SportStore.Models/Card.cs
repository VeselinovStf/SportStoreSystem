using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Models
{
    public class Card : Entity
    {
        public Card()
        {
            this.CardItems = new HashSet<CardItem>();
        }     

        public ICollection<CardItem> CardItems { get; set; }
    }
}
