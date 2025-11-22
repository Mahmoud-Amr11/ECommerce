using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.OrderModules
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            UserEmail = userEmail;           
            Address = address;
            DeliveryMethod=deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod{ get; set; }=default!;
        public int DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal{ get; set; }
        
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
