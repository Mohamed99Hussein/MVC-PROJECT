using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.ViewModels.MemberViewModels
{
    public class MemberToUpdateViewModel
    {
        [Display]
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50")]
        [RegularExpression(@"^[a-zA-Z\s]+$")]
        public string Name { get; set; } = null!;
        [Display]
        public string? Photo { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Format")] // App Validation
        [DataType(DataType.EmailAddress)] // UI Hint
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100")]

        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")] // App Validation
        [DataType(DataType.PhoneNumber)] // UI Hint
        [RegularExpression(@"^(011|012|015|010)\d{8}$", ErrorMessage = "Phone Number must be an Egyptian Phone Number")]

        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Building Number is Required")]
        [Range(1, 9000, ErrorMessage = "Building Number must be between 1 and 9000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 30")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces")]
        public string City { get; set; } = null!;

    }
}
