using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;

using UTTAF.API.Tests.Exceptions;
using UTTAF.Dependencies.Enums;
using UTTAF.Dependencies.Helpers;
using UTTAF.Dependencies.Models;
using UTTAF.Dependencies.Services;

using Xunit;

namespace UTTAF.API.Tests
{
    public class AuthAPITests
    {
        private const string SessionReference = "testSession";
        private const string SessionPassword = "test123";

        [Fact]
        public async void AuthSessionAndReturnData()
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

                Assert.Equal(SessionReference, session.SessionReference);
                Assert.Equal(SecurityService.CalculateHash256(SessionPassword), session.SessionPassword);
                Assert.Equal(SessionStatusEnum.Active, session.SessionStatus);
                Assert.NotNull(session.SessionDate);
            }
            else
                throw new InvalidSessionException(response.Content);
        }
    }
}