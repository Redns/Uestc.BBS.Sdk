using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Auth;

namespace Uestc.BBS.Sdk.Test
{
    public class AuthServiceTest
    {
        private readonly IServiceProvider _services;

        public AuthServiceTest()
        {
            var collection = new ServiceCollection();
            collection.AddAuthClient();
            collection
                .AddSingleton(services =>
                {
                    var config = services.GetRequiredService<IConfigurationRoot>();
                    return new AuthCredential
                    {
                        Username =
                            config["Username"]
                            ?? throw new ArgumentException("Username is not set"),
                        Password =
                            config["Password"]
                            ?? throw new ArgumentException("Password is not set"),
                        Cookies = new CookieContainer(),
                    };
                })
                .AddSingleton(
                    new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", true, true)
                        .AddUserSecrets<AuthServiceTest>()
                        .Build()
                );

            _services = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task LoginAsyncTest()
        {
            var authService = _services.GetRequiredService<IAuthService>();
            var credential = _services.GetRequiredService<AuthCredential>();

            credential.Cookies.GetAllCookies().Clear();
            credential.Autherization = string.Empty;
            credential.Token = string.Empty;
            credential.Secret = string.Empty;

            await authService.LoginAsync(credential);

            Assert.True(credential.Cookies.Count > 0);
            Assert.NotEmpty(credential.Autherization);
            Assert.NotEmpty(credential.Token);
            Assert.NotEmpty(credential.Secret);
        }
    }
}
