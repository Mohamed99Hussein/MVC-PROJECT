using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        [Required(ErrorMessage = "weight is Required")]
        [Range(1, 500, ErrorMessage = "weight must be greater than 0 and less than 500")]
        public decimal weight {  get; set; }

        [Required(ErrorMessage = "Height is Required")]
        [Range(1, 300, ErrorMessage = "Height must be greater than 0 and less than 300")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "BloodType is Required")]
        [StringLength(3,ErrorMessage = "BloodType must be 3 chars or less")]
        public string BloodType { get; set; } = null!;

        public string? Note { get; set; } = null!;


    }
}
