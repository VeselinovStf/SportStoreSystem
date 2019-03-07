namespace SportStore.Models
{
    public class CardItem : Entity
    {   
        public int Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int Card_Id { get; set; }

        public Card Card { get; set; }
    }
}