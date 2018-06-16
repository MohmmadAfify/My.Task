using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {
        public TContext context;
        private DbSet<TEntity> set;


        public Repository(TContext context)
        {
            this.context = context;
            set = context.Set<TEntity>();
        }

        public bool Add(TEntity entity)
        {
            set.Add(entity);
            return context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var entity = set.Find(id);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public bool Edit(TEntity entity)
        {
            set.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public IQueryable<TEntity> GetAll()
        {
            return set;
        }

        public List<TEntity> GetAllBind()
        {
            return set.ToList();
        }

        public TEntity GetById(int id)
        {
            return set.Find(id);
        }
    }
}
