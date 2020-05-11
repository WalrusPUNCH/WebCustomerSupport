using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerSupport.DAL.Impl
{
    public static class LinqExtensions
    {
        public static IQueryable<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var skip = (page - 1) * pageSize;

            return query.Skip(skip).Take(pageSize);
        }
    }
}
