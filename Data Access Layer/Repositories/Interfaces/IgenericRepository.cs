using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    // that TEntity must be a concrete class that must have a constructor.
    // By enforcing these constraints, you can ensure that class
    //  not abstracted like (BaseEntity-GymUser).

    public interface IgenericRepository<TEntity> where TEntity : class , new()
    {
        IEnumerable<TEntity> GetAll(Func<TEntity,bool>? condition = null);
        TEntity? GetById(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
