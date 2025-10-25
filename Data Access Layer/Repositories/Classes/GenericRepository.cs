using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    public class GenericRepository<TEntity> : IgenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly GymSystemDBContext context;

        public GenericRepository(GymSystemDBContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);






        public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);







        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null )
        {
            if(condition is null)
              return context.Set<TEntity>().AsNoTracking().ToList();
            else
              return context.Set<TEntity>().AsNoTracking().Where(condition).ToList();

        }

        public TEntity? GetById(int id) => context.Set<TEntity>().Find(id);
       

        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);

    }
}
