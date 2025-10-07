using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Entities
{
    // 1-1 relationship with Member [Shared p.k ]
    public class HealthRecord : BaseEntity
    {
        // Lastupdate == UpdatedAt from BaseEntity
        public decimal Weight { get; set; } // in kg
        public decimal Height { get; set; } // in cm

        public string BloodType { get; set; } = null!;

        public string? Note { get; set; }  


    }
}
