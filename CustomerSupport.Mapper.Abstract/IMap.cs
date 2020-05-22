using System;

namespace CustomerSupport.Mapper.Abstract
{
    public interface IMap<TEntity, TModel> : IMapTo<TEntity, TModel>, IMapFrom<TEntity, TModel>
    {

    }
}
