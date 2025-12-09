using Business_Logic_Layer.ViewModels.MembershipViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.MembershipService.Interface
{
    public interface IMembershipService
    {
        IEnumerable<MembershipViewModel> GetAllMemberships();
        bool CreateMembership(CreateMemberShipViewModel CreatedMemberShip);
        bool DeleteMemberShip(int MemberId);
        IEnumerable<PlanSelectListViewModel> GetPlansForDropDown();
        IEnumerable<MemberSelectListViewModel> GetMembersForDropDown();
    }
}
