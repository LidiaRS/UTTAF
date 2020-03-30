using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Threading.Tasks;

using UTTAF.Dependencies.Helpers;

using Xunit;

namespace UTTAF.API.Tests
{
    public class AttendeeAPITests
    {
        protected virtual async Task JoinAtSession(string attendee)
        {
            IRestResponse response = await new RequestService()
            {
                URL = DataHelper.URI,
                Method = Method.POST,
                URN = "Attendee/Join",
                Body = attendee
            }.ExecuteTaskAsync();

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}