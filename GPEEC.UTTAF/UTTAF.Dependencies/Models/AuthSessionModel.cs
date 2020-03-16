using System.ComponentModel.DataAnnotations;

namespace UTTAF.Dependencies.Models
{
    public class AuthSessionModel : SessionModel
    {
        [Required]
        public string SessionPassword { get; set; }
    }
}