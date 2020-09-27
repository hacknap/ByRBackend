using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Helpers
{
    public class PageAndSortResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalRows { get; set; }
    }
}
