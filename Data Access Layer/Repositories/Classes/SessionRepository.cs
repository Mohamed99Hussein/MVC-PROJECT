using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymSystemDBContext dbContext;

        public SessionRepository(GymSystemDBContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainersAndCategories()
        {
            return dbContext.Sessions
                .Include(s => s.Category)
                .Include(s => s.SessionTrainer)
                .ToList();
        }

        public int GetCountOfBookings(int sessionId)
        {
            return dbContext.MemberSessions
                .Count(ms => ms.SessionId == sessionId);
        }

        public Session? GetSessionWithCategoryAndTrainerById(int sessionId)
        {
           var Session = dbContext.Sessions
                .Include(s => s.Category)
                .Include(s => s.SessionTrainer)
                .FirstOrDefault(s => s.Id == sessionId);

            if (Session == null) return null;


            return Session;
        }
    }
}
