using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.ViewModels.MemberViewModels
{
    internal class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string? Photo { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PlanName { get; set; } 

        public string? MemberShipStartDate { get; set; } 

        public string? DateOfBirth { get; set; }
        public string? MemberShipEndDate { get; set; } 






    }
}
