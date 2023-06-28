using System.ComponentModel.DataAnnotations;

namespace Manager.Models
{
    public record PhoneName
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public required string Name { get; set; }

        [IsraeliPhoneNumber(ErrorMessage = "Phone number is not valid")]
        public required string Phone { get; set; }
    }
}
