using UTTAF.Dependencies.Data.VOs;

namespace UTTAF.Dependencies.Clients.Helpers
{
	public static class DataHelper
	{
		public const string URL = "http://localhost:5050";

		public static SessionVO AuthSession { get; set; }
		public static AttendeeVO Attendee { get; set; }
	}
}