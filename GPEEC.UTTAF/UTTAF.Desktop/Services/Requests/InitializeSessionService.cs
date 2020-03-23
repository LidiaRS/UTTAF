using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Desktop.Services.Requests
{
    internal class InitializeSessionService
    {
        internal async static Task<IRestResponse> InitSessionTaskAsync(AuthSessionModel authSession)
        {
            return await new RequestService()
            {
                URL = DataHelper.Uri,
                URN = "Session",
                Method = Method.POST,
                Body = authSession
            }.ExecuteTaskAsync();
        }
    }
}