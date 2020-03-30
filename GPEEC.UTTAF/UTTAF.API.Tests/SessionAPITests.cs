using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UTTAF.API.Tests.Exceptions;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Dependencies.Services;

using Xunit;
using Xunit.Extensions.Ordering;

namespace UTTAF.API.Tests
{
    public class SessionAPITests : AttendeeAPITests
    {
        private const string SessionReference = "testSession";
        private const string SessionPassword = "test123";

        [Fact, Order(0)]
        public async Task AuthSessionAndReturnData()
        {
            IRestResponse response = await new RequestService()
            {
                URL = DataHelper.URI,
                Method = Method.POST,
                URN = "Session",
                Body = new { SessionReference, SessionPassword }
            }.ExecuteTaskAsync();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                AuthSessionModel session = JsonSerializer.Deserialize<AuthSessionModel>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Assert.Equal(SessionReference, session.SessionReference);
                Assert.Equal(SecurityService.CalculateHash256(SessionPassword), session.SessionPassword);
                Assert.Equal(SessionStatusEnum.Active, session.SessionStatus);
                Assert.NotNull(session.SessionDate);
            }
            else
                throw new InvalidSessionException(response.Content);
        }

        [Theory, Order(1)]
        [InlineData("{ \"Name\": \"Test Name\", \"SessionReference\": \"" + SessionReference + "\"}")]
        protected override async Task JoinAtSession(string attendee) => await base.JoinAtSession(attendee);

        [Fact, Order(2)]
        public async Task RemoveSession()
        {
            IRestResponse response = await new RequestService()
            {
                URL = DataHelper.URI,
                Method = Method.DELETE,
                URN = "Session",
                Body = new { SessionReference, SessionPassword }
            }.ExecuteTaskAsync();

            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}