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
    public class MembershipRepository : IMembershipRepository
    {
        private readonly GymSystemDBContext dBContext;

        public MembershipRepository(GymSystemDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool> predicate)
        {
          return  dBContext.Memberships.Include(x=>x.Plan).Include(x=>x.Member)
                .Where(predicate).ToList();

        }
    }
}
