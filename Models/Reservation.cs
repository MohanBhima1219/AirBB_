using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirBB.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Please enter a ReservationStartDate.")]
        public DateTime ReservationStartDate { get; set; }
        
        [Required(ErrorMessage = "Please enter a ReservationEndDate.")]
        public DateTime ReservationEndDate { get; set; }

        [Required(ErrorMessage = "Please enter a Residence.")]
        public int ResidenceId { get; set; }
        [ValidateNever]
        public Residence Residence { get; set; } = null!;
    }
}
