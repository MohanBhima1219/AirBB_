using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBB.Models
{
    public class Residence
    {
        public int ResidenceId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        [RegularExpression("^[a-zA-Z0-9 ]+$")]
        [StringLength(50, ErrorMessage = "Please limit your name to 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a ClientId.")]
        [Remote("CheckOwner", "Validation", areaName: "")]
        [Display(Name = "ClientId")]
        public int ClientId { get; set; }
        //[ValidateNever]
        public Client Client { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a ResidencePicture.")]
        public string ResidencePicture { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a GuestNumber.")]
        public int GuestNumber { get; set; }
        
        [Required(ErrorMessage = "Please enter a BedroomNumber.")]
        [RegularExpression(@"^\d+(\.5)?$", ErrorMessage = "Bathroom Number must be an integer or end with .5")]
        public decimal BedroomNumber { get; set; }
        
        [Required(ErrorMessage = "Please enter a BathroomNumber.")]
        public int BathroomNumber { get; set; }

        [Required(ErrorMessage = "Please enter a date of birth.")]
        [PastYear(150, ErrorMessage = "Built year must be a past year, but no more than 150 years ago.")]
        [DataType(DataType.Date)]
        public DateTime BuiltYear { get; set; }

        [Required(ErrorMessage = "Please enter a PricePerNight.")]
        public string PricePerNight { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Location.")]
        public int LocationId { get; set; }
        [ValidateNever]
        public Location Location { get; set; } = null!;
    }
}
