using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        public int? brandId{ get; set; }
        public int? typeId{ get; set; }
        public ProductSortingOption? sortingOption{ get; set; }
        public string? searchTerm{ get; set; }
    }
}
