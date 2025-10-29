using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class Member : GymUser
    {
        // JoinDate == CreatedAt from BaseEntity 

        // Photo URL or path
        public string Photo { get; set; } = null!;

        #region Member - HealthRecord
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member - MemberShip
        public ICollection<MemberShip> MemberShips { get; set; } = null!;
        #endregion

        #region Member - MemberSession
        public ICollection<MemberSession> MemberSessions { get; set; } = null!;
        #endregion


    }
}
