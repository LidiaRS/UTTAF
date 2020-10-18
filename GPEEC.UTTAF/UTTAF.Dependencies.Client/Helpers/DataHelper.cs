using UTTAF.Dependencies.Clients.Models;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Clients.Helpers
{
	public static class DataHelper
	{
		public const string URL = "http://localhost:5050";
		public const string URLMobile = "http://10.0.2.2:5050";

		public static SessionVO AuthSession { get; set; }
		public static AttendeeModel Attendee { get; set; }
	}
}