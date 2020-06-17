using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Helpers
{
	public static class DataHelper
	{
		public static string URLBase { get; } = "http://localhost:5000/api";

		public static SessionVO AuthSession { get; set; }
		public static AttendeeVO Attendee { get; set; }
	}
}