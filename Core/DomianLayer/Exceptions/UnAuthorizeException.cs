using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class UnAuthorizeException(string? message="invalid Email or Password"):UnauthorizedAccessException(message)
    {
    }
}
