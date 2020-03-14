using System;
using System.ComponentModel.DataAnnotations;

namespace UTTAF.Dependencies.Models
{
    public class AuthModel
    {
        [Key]
        [Required]
        public string SessionReference { get; set; }

        [Required]
        public DateTime? SessionDate { get; set; }
    }
}