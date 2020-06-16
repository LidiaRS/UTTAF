using System.ComponentModel.DataAnnotations;

namespace UTTAF.API.Models
{
	public class AuthSessionModel : SessionModel
	{
		[Required]
		public string SessionPassword { get; set; }
	}
}