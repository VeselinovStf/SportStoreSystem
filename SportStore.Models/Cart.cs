using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Models
{
    public class Cart : Entity
    {
        public Cart()
        {
            this.CardItems = new HashSet<CardItem>();
        }     

        public ICollection<CardItem> CardItems { get; set; }
    }
}
