using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UTTAF.Dependencies.Services
{
    public class SecurityService
    {
        public static string CalculateHash256(string str)
        {
            var builder = new StringBuilder();

            new SHA256Managed().ComputeHash(new UTF8Encoding()
                .GetBytes(str)).ToList()
                .ForEach(a => builder.Append(a.ToString("x2")));

            return builder.ToString();
        }
    }
}