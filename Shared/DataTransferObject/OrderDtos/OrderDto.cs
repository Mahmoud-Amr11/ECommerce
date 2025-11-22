using Shared.DataTransferObject.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.OrderDtos
{
    public class OrderDto
    {
        public string BasketId{ get; set; }=default!;
        public int DelivrryMethodId{ get; set; }
        public AddressDto Address{ get; set; }=default!;
    }
}
