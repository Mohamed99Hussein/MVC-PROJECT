using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    internal class MemberSession : BaseEntity
    {
        // BookingDate == CreatedAt from BaseEntity
        #region Member - MemberSession
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        #endregion

        #region Session - MemberSession
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        #endregion

        public bool IsAttended { get; set; } 
    }
}
