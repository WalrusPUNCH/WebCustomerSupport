using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSupport.Mapper.Abstract
{
    public interface IMapFrom<out TEntity, in TModel>
    {
        TEntity MapFrom(TModel model);
    }
}
