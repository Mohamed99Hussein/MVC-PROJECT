using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.ViewModels.PlanViewModels
{
    public class PlanToUpdateViewModel
    {
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Your name must be bigger than 3 and less than 50")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Description must be bigger than 3 and less than 200")]

        public string Description { get; set; } = null!;

         [Required(ErrorMessage = "DurationDays is Required")]
        [Range(1,365,ErrorMessage ="The Value must be between 1 and 365")]
        public int DurationDays { get; set; }


        [Required(ErrorMessage = "Price is Required")]
        public decimal Price { get; set; }



    }
}
