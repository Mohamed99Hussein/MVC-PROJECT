using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    internal interface IMemberSessionRepository
    {
        // Get all MemberSessions
        IEnumerable<MemberSession> GetAllMemberSession();

        //  Get MemberSession by id
        MemberSession? GetMemberSession(int id);

        //  Add MemberSession
        int AddMemberSession(MemberSession memberSession);

        //  Update MemberSession
        int UpdateMemberSession(MemberSession memberSession);

        //  Delete MemberSession
        int DeleteMemberSession(int id);

    }
}
