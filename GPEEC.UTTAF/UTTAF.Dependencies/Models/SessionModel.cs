using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using UTTAF.Dependencies.Enums;

namespace UTTAF.Dependencies.Models
{
    public class SessionModel
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