using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    public class Plan : BaseEntity
    {

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } 
        public int DurationDays { get; set; } 

        bool IsActive { get; set; }

        #region Plan - MemberShip
        public ICollection<MemberShip> MemberShips { get; set; } = null!;
        #endregion
    }
}
