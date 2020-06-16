using Dependencies;
using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Data.VOs;
using UTTAF.Dependencies.Helpers;

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

            request.Parameters.Add(nameof(SessionVO.SessionReference), reference);

            return await request.ExecuteTaskAsync();
        }
    }
}