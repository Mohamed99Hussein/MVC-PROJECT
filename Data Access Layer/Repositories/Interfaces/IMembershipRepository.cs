using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    public interface IMembershipRepository
        //: IgenericRepository<MemberShip>
    {
    IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool> predicate);

    }
}
