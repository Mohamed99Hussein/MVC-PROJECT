using Data_Access_Layer.Data.Contexts;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Classes
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly GymSystemDBContext context;
        public SessionRepository(GymSystemDBContext context)
        {
            this.context = context;
        }
        public int AddSession(Session session)
        {
            context.Sessions.Add(session);
            return context.SaveChanges();

        }

        public int DeleteSession(int id)
        {
            var session = context.Sessions.Find(id);
            if (session != null)
            {
                context.Sessions.Remove(session);
                return context.SaveChanges();
            }
            return 0;

        }

        public IEnumerable<Session> GetAllSession()
        {
            return context.Sessions.ToList();

        }

        public Session? GetSession(int id)
        {
            return context.Sessions.Find(id);

        }

        public int UpdateSession(Session session)
        {
            context.Sessions.Update(session);
            return context.SaveChanges();

        }
    }
}
