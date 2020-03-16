using System.ComponentModel.DataAnnotations;

using UTTAF.Dependencies.Models;

namespace UTTAF.API.Models
{
    public class AuthSessionModel : SessionModel
    {
        [Required]
        public string SessionPassword { get; set; }
    }
}