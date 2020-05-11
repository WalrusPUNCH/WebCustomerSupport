using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Elements { get; set; }
        public override int PageCount { get; set;}

        public PagedResult()
        {
            Elements = new List<T>();
        }

    }
}
