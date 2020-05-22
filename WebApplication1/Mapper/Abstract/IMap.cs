using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Web.Mapper.Abstract
{
    public interface IMap<TEntity, TModel> : IMapTo<TEntity, TModel>, IMapFrom<TEntity, TModel>
    {

    }
}
