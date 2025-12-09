using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        // Get all members
        IEnumerable<Member> GetAllMembers();

        //  Get member by id
        Member? GetMember(int id);

        //  Add member
        int AddMember(Member member);

        //  Update member
        int UpdateMember(Member member);

        //  Delete member
        int DeleteMember(int id);


    }
}
