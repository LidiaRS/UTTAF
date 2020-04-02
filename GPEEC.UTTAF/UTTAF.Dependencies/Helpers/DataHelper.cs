using UTTAF.Dependencies.Models;

namespace UTTAF.Dependencies.Helpers
{
    public static class DataHelper
    {
        public static string URLBase { get; } = "192.168.1.10:5000/api";

        public static AuthSessionModel AuthSession { get; set; }
        public static AttendeeModel Attendee { get; set; }
    }
}