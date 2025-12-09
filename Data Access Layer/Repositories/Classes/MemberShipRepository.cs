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
    internal class MemberShipRepository : IMemberShipRepository
    {
        private readonly GymSystemDBContext context;

        public MemberShipRepository(GymSystemDBContext context)
        {
            this.context = context;
        }

        public int AddMemberShip(MemberShip memberShip)
        {
            context.Memberships.Add(memberShip);
           return context.SaveChanges();

        }

        public int DeleteMemberShip(int id)
        {
            var memberShip = context.Memberships.Find(id);
            if (memberShip != null)
            {
                context.Memberships.Remove(memberShip);
                return context.SaveChanges();
            }
            return 0;


        }

        public IEnumerable<MemberShip> GetAllMemberShip()
        {
           return context.Memberships.ToList();
        }

        public MemberShip? GetMemberShip(int id)
        {
            return context.Memberships.Find(id);
        }

        public int UpdateMemberShip(MemberShip memberShip)
        {
            var existingMemberShip = context.Memberships.Find(memberShip.Id);
            if (existingMemberShip == null)
            {
                return 0; // MemberShip not found
            }
            context.Memberships.Update(memberShip);
            return context.SaveChanges();
        }
    }
}
