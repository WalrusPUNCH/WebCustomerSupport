using System;
using System.Collections.Generic;


namespace CustomerSupport.DAL.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity item);
        bool Delete(int id);
    }
}
