using System;
using System.ComponentModel.DataAnnotations;

namespace UTTAF.API.Models.Auxiliary
{
    public class CardModel
    {
        [Required]
        public string SessionReference { get; set; }

        [Required]
        public string SessionPassword { get; set; }

        [Required]
        public string CardId { get; set; }

        [Required]
        public DateTime RegistredTime { get; set; }
    }
}