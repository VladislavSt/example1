using Xunit;
using Yam.API.Model.Dtos.Account.Response;
using Yam.API.Model.Dtos.Account.Request;
using Yam.API.Model.Dtos.Account;
using Yam.Core.Extensions;

namespace Yam.Tests.IntegrationTests
{
    public class AccountTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public AccountTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetInfo_After_Registration_And_Login()
        {
            // registration
            using var httpClient = _factory.CreateClient();

            var id = Guid.NewGuid();

            var registerUserDTO = new RegisterUserDTO()
            {
                Email = $"email{id}@gmail.com",
                Password = $"ssdfgfdgAbc99@{id}",
                Username = $"email1{id}"
            };

            var regResponse = await httpClient.PostAsync("api/account/register", registerUserDTO.ToHttpStringContent());

            var regResult = await regResponse.Content.ReadAsAsync<RegisterResponseDTO>();

            Assert.True(regResult.Success);

            // login

            var loginUserDTO = new LoginUserDTO()
            {
                Email = registerUserDTO.Email,
                Password = registerUserDTO.Password,
            };

            var authResponse = await httpClient.PostAsync("api/account/login", loginUserDTO.ToHttpStringContent());

            var authResult = await authResponse.Content.ReadAsAsync<AuthResult>();

            // get user info

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authResult.Token);

            var infoResponse = await httpClient.GetAsync("api/account/info");

            var userInfo = await infoResponse.Content.ReadAsAsync<UserResponseDTO>();

            Assert.Equal(userInfo.Email, registerUserDTO.Email);
            Assert.Equal(userInfo.Username, registerUserDTO.Username);
        }
    }
}
