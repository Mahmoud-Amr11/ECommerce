namespace DomainLayer.Models.BasketModule
{
    public class Basket
    {
        public Guid Id { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; } = [];
    }
}
