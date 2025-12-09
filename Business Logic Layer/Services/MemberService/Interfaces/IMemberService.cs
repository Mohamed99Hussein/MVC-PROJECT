using Business_Logic_Layer.ViewModels.MemberViewModels;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.MemberService.Interfaces
{
    internal interface IMemberService 
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel CreateMember);

        MemberViewModel? GetMemberDetails(int MemberId);

        HealthRecordViewModel? GetHealthRecord(int MemberId);


        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel UpdatedMember);

    }
}
