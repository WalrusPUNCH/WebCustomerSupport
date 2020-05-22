using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Web.Mapper.Abstract
{
    public interface IMapFrom<out TEntity, in TModel>
    {
        TEntity MapFrom(TModel model);
    }
}
