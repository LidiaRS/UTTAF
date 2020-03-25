using Dependencies.Services;

using NUnit.Framework;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UTTAF.API.Tests.Exceptions;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Dependencies.Services;

namespace UTTAF.API.Tests
{
    public class AuthAPITests
    {
        private const string SessionReference = "testSession";
        private const string SessionPassword = "test123";

        [Test, Order(0)]
        public async Task AuthSessionAndReturnData()
        {
            IRestResponse response = await new RequestService()
            {
                URL = DataHelper.Uri,
                Method = Method.POST,
                URN = "Session",
                Body = new { SessionReference, SessionPassword }
            }.ExecuteTaskAsync();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                AuthSessionModel session = JsonSerializer.Deserialize<AuthSessionModel>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Assert.AreEqual(SessionReference, session.SessionReference);
                Assert.AreEqual(SecurityService.CalculateHash256(SessionPassword), session.SessionPassword);
                Assert.AreEqual(SessionStatusEnum.Active, session.SessionStatus);
                Assert.NotNull(session.SessionDate);
            }
            else
                throw new InvalidSessionException(response.Content);
        }

        [Test, Order(1)]
        public async Task RemoveSession()
        {
            IRestResponse response = await new RequestService()
            {
                URL = DataHelper.Uri,
                Method = Method.DELETE,
                URN = "Session",
                Body = new { SessionReference, SessionPassword }
            }.ExecuteTaskAsync();

            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}