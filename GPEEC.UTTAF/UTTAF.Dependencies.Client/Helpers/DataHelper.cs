using UTTAF.Dependencies.Clients.Models;
using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Clients.Helpers
{
	public static class DataHelper
	{
		public const string URL = "http://192.168.1.99:5050";
		public const string URLMobile = "http://192.168.1.99:5050";

		public static SessionVO AuthSession { get; set; }
		public static AttendeeModel Attendee { get; set; }
	}
}