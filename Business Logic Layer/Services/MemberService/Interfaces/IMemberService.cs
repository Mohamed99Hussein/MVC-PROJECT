using Business_Logic_Layer.ViewModels.MemberViewModels;
using Data_Access_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.MemberService.Interfaces
{
    public interface IMemberService 
    {
        public IEnumerable<MemberViewModel> GetAllMembers();

        public bool CreateMember(CreateMemberViewModel CreateMember);

        public MemberViewModel? GetMemberDetails(int MemberId);

        public HealthRecordViewModel? GetHealthRecord(int MemberId);


        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        public bool UpdateMemberDetails(int MemberId, MemberToUpdateViewModel UpdatedMember);

        public bool RemoveMember(int MemberId);

    }
}
