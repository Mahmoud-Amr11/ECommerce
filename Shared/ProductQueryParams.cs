using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {

        private const int  MaxPageSize = 10;
        private const int defaultPageSize = 5;
        public int? brandId{ get; set; }
        public int? typeId{ get; set; }
        public ProductSortingOption? sortingOption{ get; set; }
        public string? searchTerm{ get; set; }
        public int PageIndex { get; set; } = 1;

        private int pageSize = defaultPageSize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }
}
