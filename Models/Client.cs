using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Models
{
    public class Client : IValidatableObject
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        
        [Remote("CheckEmail", "Validation", areaName: "")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Please enter a SSN.")]
        public string SSN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a DOB.")]
        [MinimumAge(13, ErrorMessage = "You must be at least 13 years old.")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        
        [Required(ErrorMessage = "Please enter a UserType.")]
        public string UserType { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber) && string.IsNullOrWhiteSpace(Email))
            {
                yield return new ValidationResult(
                    "Either Phone Number or Email must be provided.",
                    new[] { nameof(PhoneNumber), nameof(Email) });
            }
        }
    }
}
