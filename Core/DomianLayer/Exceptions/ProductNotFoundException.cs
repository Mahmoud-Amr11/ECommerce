using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class ProductNotFoundException(int id):NotFoundException($"The product you are looking for does not exist with Id {id}")
    {
    }
}
