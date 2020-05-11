using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSupport.BL.Services.Mapper.Abstract
{
    public interface IBLMapper
    {
        TDestination MapOne<TDestination>(object sourceItem);
        IEnumerable<TDestination> MapMany<TDestination>(IEnumerable<object> sourceCollection);
    }
}
