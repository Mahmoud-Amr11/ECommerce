namespace Shared.DataTransferObject.BasketDtos
{
    public class BasketDto
    {

        public string Id { get; set; }
        public ICollection<BasketItemDto> BasketItems { get; set; } = [];
        public string? PaymentIntendId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
