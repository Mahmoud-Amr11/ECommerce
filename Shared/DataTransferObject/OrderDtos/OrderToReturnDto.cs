using Shared.DataTransferObject.IdentityDto;

namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }

        public string UserEmail{ get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto Address { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string OrderStatus { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];
     
    }
}
