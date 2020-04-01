using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using UTTAF.Dependencies.Enums;

namespace UTTAF.API.Models
{
    public class RobotModel
    {
        [Key]
        public Guid RobotId { get; set; }

        [Required]
        public string SessionReference { get; set; }

        [NotNull]
        public RobotStatusEnum RobotStatus { get; set; }

        [NotNull]
        public DateTime DataOperation { get; set; }
    }
}