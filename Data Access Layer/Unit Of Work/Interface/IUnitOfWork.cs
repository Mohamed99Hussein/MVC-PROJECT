using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Unit_Of_Work.Interface
{
    public interface IUnitOfWork
    {



        IgenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
      

        int SaveChanges();
       



    }
}
