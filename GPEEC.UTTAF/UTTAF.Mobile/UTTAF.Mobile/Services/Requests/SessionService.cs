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
                URL = DataHelper.URI,
                URN = "Session/Started",
                ContainsParameter = true,
                Method = Method.GET
            };

            request.Parameters.Add(nameof(SessionModel.SessionReference), reference);

            return await request.ExecuteTaskAsync();
        }
    }
}