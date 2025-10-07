using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class MemberShip : BaseEntity
    {
        // Membership StartDate == CreatedAt from BaseEntity

        #region Plan - MemberShip
        public int PlanId { get; set; }

        public Plan Plan { get; set; } = null!; 
        #endregion

        #region Member - MemberShip
        public int MemberId { get; set; }

        public Member Member { get; set; } = null!;
        #endregion

        public DateTime EndDate { get; set; }

        // Is the membership currently active [ReadOnly property]

        public string Status 
            { 
                get
                {
                    return DateTime.Now <= EndDate ? "Active" : "Expired";
                }
            }

    }
}
