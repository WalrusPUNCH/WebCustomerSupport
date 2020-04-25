using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Interfaces
{
    interface IBaseRepository<TEntity> where TEntity : class, IHasId
    {
        int Insert(TEntity item);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity item);
        bool Delete(int id);
    }
}
