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

        [NotNull]
        public SessionStatusEnum SessionStatus { get; set; }

        [NotNull]
        public DateTime? SessionDate { get; set; }
    }
}