using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Classes;
using Data_Access_Layer.Repositories.Interfaces;
using Data_Access_Layer.Unit_Of_Work.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Unit_Of_Work.Class
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymSystemDBContext dbContext;
        

        public UnitOfWork(GymSystemDBContext dBContext,ISessionRepository SessionRepository,IMembershipRepository _membershipRepository ,IBookingRepository bookingRepository)
        {
            this.dbContext = dBContext;
            sessionRepository = SessionRepository;
            MembershipRepository = _membershipRepository;
            BookingRepository = bookingRepository;
        }

        private readonly Dictionary<Type,object> Repositories = new();

        public ISessionRepository sessionRepository { get; }
        public IMembershipRepository MembershipRepository { get;}
        public IBookingRepository BookingRepository { get; }

        public IgenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);

            if (Repositories.TryGetValue(EntityType, out var repository))
                return (IgenericRepository<TEntity>)repository;

            var NewRepository = new GenericRepository<TEntity>(dbContext);
              Repositories[EntityType] = NewRepository;
            
                return NewRepository;

            


        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
    }
}
