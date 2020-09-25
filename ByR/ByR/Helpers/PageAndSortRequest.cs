using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Helpers
{
    public class PageAndSortRequest
    {
        public string Column { get; set; }
        public string Direction { get; set; }
        public int Page { get; set; }
        public int SizePage { get; set; }
        public string Filter { get; set; }
    }
}
