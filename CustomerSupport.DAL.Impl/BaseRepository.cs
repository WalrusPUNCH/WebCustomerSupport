using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CustomerSupport.DAL.Abstract;
using CustomerSupport.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerSupport.DAL.Impl
{
    public class BaseRepository<TEntity>:  IBaseRepository<TEntity> where TEntity: HasID
    {
        private readonly CustomerSupportContext context;
        private DbSet<TEntity> set;
        private DbSet<TEntity> DBSet
        {
            get
            {
                if (set == null)
                    return context.Set<TEntity>();
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

        public virtual void Delete(int id)
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

        public virtual TEntity FindByID(int id)
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
    }
}
