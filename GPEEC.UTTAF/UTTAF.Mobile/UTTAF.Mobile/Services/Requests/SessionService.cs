using Dependencies;
using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Mobile.Services.Requests
{
    internal class SessionService
    {
        internal static async Task<IRestResponse> SessionStartedTaskAsync(string reference)
        {
            var request = new RequestService()
            {
                Protocol = Protocols.HTTP,
                URL = DataHelper.URLBase,
                URN = "Session/Started",
                Method = Method.GET
            };

            request.Parameters.Add(nameof(SessionModel.SessionReference), reference);

            return await request.ExecuteTaskAsync();
        }
    }
}