using System;
using System.ComponentModel.DataAnnotations;

namespace UTTAF.Dependencies.Models
{
    public class AttendeeModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string SessionReference { get; set; }
    }
}