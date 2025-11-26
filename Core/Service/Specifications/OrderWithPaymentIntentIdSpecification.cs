using DomainLayer.Models.OrderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class OrderWithPaymentIntentIdSpecification : BaseSpecification<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecification(string paymentInentId):base(o=>o.PaymentIntendId==paymentInentId)
        {
            
        }
    }
}
