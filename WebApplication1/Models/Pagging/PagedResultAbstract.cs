using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public virtual int PageCount { get; set; }
        public int PageSize { get; set; } = 5;
    }
}
