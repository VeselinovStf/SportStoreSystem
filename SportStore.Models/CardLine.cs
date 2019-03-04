﻿namespace SportStore.Models
{
    public class CardLine
    {
        public int CardLineID { get; set; }

        public int ProductID { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

        public int Card_Id { get; set; }

        public Card Card { get; set; }
    }
}