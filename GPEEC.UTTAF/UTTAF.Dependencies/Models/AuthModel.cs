using System;
using System.ComponentModel.DataAnnotations;

namespace UTTAF.Dependencies.Models
{
    public class AuthModel
    {
        [Key]
        public Guid SessionId { get; set; }

        [Required]
        public string SessionName { get; set; }

        [Required]
        public string SessionPassword { get; set; }

        [Required]
        public DateTime? SessionDate { get; set; }
    }
}