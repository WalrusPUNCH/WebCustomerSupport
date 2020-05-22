using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Specifications.Abstract;

namespace CustomerSupport.DAL.Impl
{
    public class BaseRepository<TKey, TEntity>: IBaseRepository<TKey, TEntity> where TEntity: class
    {
        private readonly CustomerSupportContext context;
        private DbSet<TEntity> set;
        private DbSet<TEntity> DBSet
        {
            get
            {
                if (set == null)
                {
                    set =  context.Set<TEntity>();
                    return set;
                }
                else
                    return set;
            }
        }
        public BaseRepository(CustomerSupportContext context)
        {
            this.context = context;
        }
       
        public virtual void Add(TEntity item)
        {
            DBSet.Add(item); 
        }

        public virtual void Delete(TKey id)
        {
            TEntity item = FindByID(id);
            if (item != null)
                DBSet.Remove(item);
        }

        public virtual IEnumerable<TEntity> GetAll(int page, int pageSize)
        {
            return DBSet.GetPaged(page, pageSize).ToList();
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DBSet;
        }

        public virtual TEntity FindByID(TKey id)
        {
            return DBSet.Find(id);
        }

        public virtual void Update(TEntity item)
        {
            DBSet.Update(item);
        }

        public virtual int Count()
        {
            return DBSet.Count();
        }

        public IEnumerable<TEntity> GetFiltered(ISpecification<TEntity> filter)
        {
            if (DBSet.Count() == 0)
                return null;

            var queryableResultWithIncludes = filter.Includes.Aggregate(DBSet.AsQueryable(),
           (current, include) => current.Include(include));
            return queryableResultWithIncludes.Where(filter.Criteria).AsEnumerable();
        }
    }
}
