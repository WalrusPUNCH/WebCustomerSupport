using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Web.Mapper.Abstract
{
    public interface IMapTo<in TEntity, out TModel>
    {
        TModel MapTo(TEntity entity);
    }
}
