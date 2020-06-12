using Dependencies;
using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Desktop.Services.Requests
{
    public class SessionService
    {
        public async Task<IRestResponse> InitSessionTaskAsync(AuthSessionModel authSession)
        {
            return await new RequestService()
            {
                Protocol = Protocols.HTTP,
                URL = DataHelper.URLBase,
                URN = "Session",
                Method = Method.POST,
                Body = authSession
            }.ExecuteTaskAsync();
        }

        public async Task<IRestResponse> StartSessionTaskAsync(AuthSessionModel authSession)
        {
            return await new RequestService()
            {
                Protocol = Protocols.HTTP,
                URL = DataHelper.URLBase,
                URN = "Session/Status",
                Method = Method.PUT,
                Body = authSession
            }.ExecuteTaskAsync();
        }

        public async Task<IRestResponse> DeleteSessionTaskAsync(AuthSessionModel model)
        {
            return await new RequestService()
            {
                Protocol = Protocols.HTTP,
                URL = DataHelper.URLBase,
                URN = "Session",
                Method = Method.DELETE,
                Body = model
            }.ExecuteTaskAsync();
        }
    }
}