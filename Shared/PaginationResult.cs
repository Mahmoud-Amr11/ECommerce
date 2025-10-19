using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginationResult<TEntity>
    {
        public int PageSize{ get; set; }
        public int PageIndex { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Data{ get; set; }
    }
}
