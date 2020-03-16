using System.ComponentModel.DataAnnotations;

namespace UTTAF.Dependencies.Models
{
    public class AttendeeModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SessionReference { get; set; }
    }
}