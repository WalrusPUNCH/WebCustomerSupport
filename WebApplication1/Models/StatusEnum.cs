using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public enum StatusEnum
    {
        Queued = 0,         // there are no avaliable specialists
        Processing = 1,     // specialist is curently working on request
        Processed = 2       // request is no more active
    }
}
