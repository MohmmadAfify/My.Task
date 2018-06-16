using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        List<TEntity> GetAllBind();
        TEntity GetById(int id);
        bool Add(TEntity entity);
        bool Edit(TEntity entity);
        bool Delete(int id);
    }
}
