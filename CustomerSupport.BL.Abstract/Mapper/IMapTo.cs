using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSupport.BL.Abstract.Mapper
{
    public interface IMapTo<in TEntity, out TModel>
    {
        TModel MapTo(TEntity entity);
    }
}
