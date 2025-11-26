namespace DomainLayer.Models.BasketModule
{
    public class Basket
    {
        public string Id { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; } = [];
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
