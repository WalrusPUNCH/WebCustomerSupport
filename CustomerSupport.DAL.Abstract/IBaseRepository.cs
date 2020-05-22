using System;
using System.Collections.Generic;

using CustomerSupport.DAL.Entities;
using CustomerSupport.DAL.Specifications.Abstract;

namespace CustomerSupport.DAL.Abstract
{
    public interface IBaseRepository<TKey, TEntity> where TEntity : class
    {
        void Add(TEntity item);
        TEntity FindByID(TKey id);
        IEnumerable<TEntity> GetAll(int page, int pageSize);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetFiltered(ISpecification<TEntity> filter);
        void Update(TEntity item);
        void Delete(TKey id);
        int Count();

    }
}
