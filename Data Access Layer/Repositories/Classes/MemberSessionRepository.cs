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
    internal class MemberSessionRepository : IMemberSessionRepository
    {
        private readonly GymSystemDBContext context;
        public MemberSessionRepository(GymSystemDBContext context)
        {
            this.context = context;
        }

        public int AddMemberSession(MemberSession memberSession)
        {
            context.MemberSessions.Add(memberSession);
            return context.SaveChanges();

        }

        public int DeleteMemberSession(int id)
        {
            var memberSession = context.MemberSessions.Find(id);
            if (memberSession != null)
            {
                context.MemberSessions.Remove(memberSession);
                return context.SaveChanges();
            }
            return 0;

        }

        public IEnumerable<MemberSession> GetAllMemberSession()
        {
            return context.MemberSessions.ToList();

        }

        public MemberSession? GetMemberSession(int id)
        {
            return context.MemberSessions.Find(id);

        }

        public int UpdateMemberSession(MemberSession memberSession)
        {
            var existingMemberSession = context.MemberSessions.Find(memberSession.Id);
            if (existingMemberSession != null)
            {
                context.MemberSessions.Update(memberSession);
                return context.SaveChanges();
            }
            return 0;

        }
    }
}
