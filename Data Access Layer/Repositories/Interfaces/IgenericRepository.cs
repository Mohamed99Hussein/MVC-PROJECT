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

    internal interface IgenericRepository<TEntity> where TEntity : class , new()
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(int id);
        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(int id);

    }
}
