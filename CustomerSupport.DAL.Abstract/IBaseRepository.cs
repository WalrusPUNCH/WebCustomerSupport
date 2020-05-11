using System;
using System.Collections.Generic;

using CustomerSupport.DAL.Entities;


namespace CustomerSupport.DAL.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : HasID
    {
        void Add(TEntity item);
        TEntity FindByID(int id);
        //TEntity GetByIdSlim(int id);
        IEnumerable<TEntity> GetAll(int page, int pageSize);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity item);
        void Delete(int id);
        int Count();
    }
}
