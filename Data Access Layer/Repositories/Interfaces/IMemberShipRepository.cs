using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.Interfaces
{
    internal interface IMemberShipRepository
    {
        // Get all MemberShips
        IEnumerable<MemberShip> GetAllMemberShip();

        //  Get MemberShip by id
        MemberShip? GetMemberShip(int id);

        //  Add MemberShip
        int AddMemberShip(MemberShip memberShip);

        //  Update MemberShip
        int UpdateMemberShip(MemberShip memberShip);

        //  Delete MemberShip
        int DeleteMemberShip(int id);
    }
}
