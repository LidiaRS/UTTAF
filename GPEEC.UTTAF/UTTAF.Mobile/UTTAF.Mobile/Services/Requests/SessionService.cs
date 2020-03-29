using Dependencies.Services;

using RestSharp;

using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;

namespace UTTAF.Mobile.Services.Requests
{
    internal class SessionService
    {
        internal async static Task<IRestResponse> JoinAtSession(AttendeeModel attendee)
        {
            return await new RequestService()
            {
                URL = DataHelper.URI,
                URN = "Session/Attendees",
                Body = attendee,
                Method = Method.POST
            }.ExecuteTaskAsync();
        }
    }
}