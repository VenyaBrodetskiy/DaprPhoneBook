using System.ComponentModel.DataAnnotations;

namespace Manager.Models
{
    public record PhoneName
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Phone cannot be empty")]
        public required string Phone { get; set; }
    }
}
