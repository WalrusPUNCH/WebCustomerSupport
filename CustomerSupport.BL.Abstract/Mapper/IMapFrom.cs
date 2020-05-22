using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSupport.BL.Abstract.Mapper
{
    public interface IMapFrom<out TEntity, in TModel>
    {
        TEntity MapFrom(TModel model);
    }
}
