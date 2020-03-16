using System;
using System.ComponentModel.DataAnnotations;

using UTTAF.Dependencies.Enums;

namespace UTTAF.Dependencies.Models
{
    public class AuthModel
    {
        [Key]
        [Required]
        public string SessionReference { get; set; }

        [Required]
        public SessionStatusEnum SessionStatus { get; set; }

        [Required]
        public DateTime? SessionDate { get; set; }
    }
}