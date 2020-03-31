using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Desktop.Services.Requests
{
    internal class SessionService
    {
        internal static async Task<IRestResponse> InitSessionTaskAsync(AuthSessionModel authSession)
        {
            return await new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Session",
                Method = Method.POST,
                Body = authSession
            }.ExecuteTaskAsync();
        }
        internal static async Task<IRestResponse> StartSessionTaskAsync(AuthSessionModel authSession)
        {
            return await new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Session/Status",
                Method = Method.PUT,
                Body = authSession
            }.ExecuteTaskAsync();
        }

        internal static async Task<IRestResponse> DeleteSessionTaskAsync(AuthSessionModel model)
        {
            return await new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Session",
                Method = Method.DELETE,
                Body = model
            }.ExecuteTaskAsync();
        }
    }
}