using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerSupport.BL.Abstract.Mapper
{
    public interface IMap<TEntity, TModel> : IMapTo<TEntity, TModel>, IMapFrom<TEntity, TModel>
    {

    }
}
