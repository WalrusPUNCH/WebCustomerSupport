using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCustomerSupportApi.Mapper.Abstract
{
    public interface IMapFrom<out TEntity, in TModel>
    {
        TEntity MapFrom(TModel model);
    }
}
