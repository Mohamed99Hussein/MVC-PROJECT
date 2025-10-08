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
    internal class GenericRepository<TEntity> : IgenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly GymSystemDBContext context;

        public GenericRepository(GymSystemDBContext context)
        {
            this.context = context;
        }

        public int Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return context.SaveChanges();

        }

        public int Delete(int id)
        {
            var entity = context.Set<TEntity>().Find(id);

            if (entity is null)
            {
                return 0; 
            }

            context.Set<TEntity>().Remove(entity);
            return context.SaveChanges();

        }

        public IEnumerable<TEntity> GetAll() => context.Set<TEntity>().AsNoTracking().ToList();


        public TEntity? GetById(int id) => context.Set<TEntity>().Find(id);
       

        public int Update(TEntity entity)
        {
            var entry = context.Set<TEntity>().Find(entity);
            if (entry is not null)
                context.Set<TEntity>().Update(entity);
                
           return context.SaveChanges(); 

        }
    }
}
