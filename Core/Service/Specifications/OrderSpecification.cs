using DomainLayer.Models.OrderModules;

namespace Service.Specifications
{
    public class OrderSpecification:BaseSpecification<Order,Guid>
    {
        public OrderSpecification(string email) :base(o => o.UserEmail == email)
        { 
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
           AddOrderByDescending(o => o.OrderDate);

        }
        public OrderSpecification(Guid id):base(o=>o.ID==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);

        }
    }
}
