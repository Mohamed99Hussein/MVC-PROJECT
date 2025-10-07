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
    internal class MemberRepository : IMemberRepository
    {
        
        private readonly GymSystemDBContext DbContext;
        public MemberRepository(GymSystemDBContext context)
        {
            DbContext = context;

        }
        // Implement member-specific data access methods here
        public int AddMember(Member member)
        {
            DbContext.Members.Add(member);
            return DbContext.SaveChanges();// Returns how many rows were affected

        }

        public int DeleteMember(int id)
        {
            var MemberToDelete = DbContext.Members.Find(id);
            if (MemberToDelete == null) return 0;
            DbContext.Members.Remove(MemberToDelete);
            return DbContext.SaveChanges();// Returns how many rows were affected
        }

        public IEnumerable<Member> GetAllMembers()
        {
            if (!DbContext.Members.Any())
                return Enumerable.Empty<Member>();

            return DbContext.Members.ToList();
        }

        public Member? GetMember(int id)
        {
           
            return DbContext.Members.Find(id);
        }

        public int UpdateMember(Member member)
        {
            var MemberToUpdate = DbContext.Members.Find(member.Id);
            if (MemberToUpdate == null) return 0;
            DbContext.Members.Update(MemberToUpdate);
            return DbContext.SaveChanges();// Returns how many rows were affected


        }
    }
}
