using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Desktop.Services.Requests
{
    internal class SessionService
    {
        internal async static Task<IRestResponse> InitSessionTaskAsync(AuthSessionModel authSession)
        {
            return await new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Session",
                Method = Method.POST,
                Body = authSession
            }.ExecuteTaskAsync();
        }

        internal async static Task<IRestResponse> GetAttendeesTaskAsync(AuthSessionModel model)
        {
            var request = new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Session/Attendees",
                Method = Method.GET,
                ContainsParameter = true
            };
            request.Parameters.Add(nameof(model.SessionReference), model.SessionReference);
            request.Parameters.Add(nameof(model.SessionPassword), model.SessionPassword);

            return await request.ExecuteTaskAsync();
        }

        internal async static Task<IRestResponse> DeleteSessionTaskAsync(AuthSessionModel model)
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