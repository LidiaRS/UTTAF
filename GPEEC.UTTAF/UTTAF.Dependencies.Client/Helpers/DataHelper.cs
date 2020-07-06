using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Clients.Helpers
{
	public static class DataHelper
	{
		public static string URL { get; } = "http://192.168.1.6:5050";

		public static SessionVO AuthSession { get; set; }
		public static AttendeeVO Attendee { get; set; }
	}
}