namespace SportStore.Models
{
    public class CardItem : Entity
    {   
      

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int Card_Id { get; set; }

        public Cart Card { get; set; }
    }
}