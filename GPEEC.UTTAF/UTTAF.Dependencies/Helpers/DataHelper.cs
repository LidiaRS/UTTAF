using UTTAF.Dependencies.Models;

namespace UTTAF.Dependencies.Helpers
{
    public static class DataHelper
    {
        public static string Uri { get; } = "http://localhost:5000/api";

        public static AuthSessionModel AuthSession { get; set; }
    }
}