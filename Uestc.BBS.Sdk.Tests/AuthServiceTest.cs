using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Services.Auth;

namespace Uestc.BBS.Sdk.Tests
{
    public class AuthServiceTest
    {
        private readonly IServiceProvider _services;

        private const string BASRE_URL = "https://bbs.uestcer.org";

        public AuthServiceTest()
        {
            var collection = new ServiceCollection();
            collection
                .AddSingleton(
                    // NOTE 需要在 appsettings.json 或用户机密中设置 Username 和 Password
                    new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", true, true)
                        .AddUserSecrets<AuthServiceTest>()
                        .Build()
                )
                .AddAuthService(BASRE_URL);

            _services = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task LoginAsyncTest()
        {
            var authService = _services.GetRequiredService<IAuthService>();
            var config = _services.GetRequiredService<IConfigurationRoot>();
            var credential = new AuthCredential
            {
                Username = config["Username"] ?? throw new ArgumentException("Username is not set"),
                Password = config["Password"] ?? throw new ArgumentException("Password is not set"),
            };

            await authService.LoginAsync(credential);

            Assert.NotEmpty(credential.Cookie);
            Assert.NotEmpty(credential.Authorization);
            Assert.NotEmpty(credential.Token);
            Assert.NotEmpty(credential.Secret);
        }
    }
}
