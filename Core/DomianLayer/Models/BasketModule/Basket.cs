namespace DomainLayer.Models.BasketModule
{
    public class Basket
    {
        public string Id { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; } = [];
    }
}
