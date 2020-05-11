using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Mapper.Abstract
{
    public interface IPLMapper
    {
        TDestination MapOne<TDestination>(object sourceItem);
        IEnumerable<TDestination> MapMany<TDestination>(IEnumerable<object> sourceCollection);
    }
}
