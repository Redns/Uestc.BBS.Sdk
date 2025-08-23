using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Services.Auth;

namespace Uestc.BBS.Sdk.Tests
{
    public class ThreadContentTest
    {
        private readonly IServiceProvider _services;

        private readonly AuthCredential _authCredential = new();

        private const string BASRE_URL = "https://bbs.uestcer.org";

        public ThreadContentTest()
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
                .AddSingleton(_authCredential)
                .AddMobcentThreadContentService(services => new Uri(BASRE_URL));

            _services = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task GetThreadContentAsyncTest()
        {
            var config = _services.GetRequiredService<IConfigurationRoot>();

            _authCredential.Username =
                config["Username"] ?? throw new ArgumentException("Username is not set");
            _authCredential.Password =
                config["Password"] ?? throw new ArgumentException("Password is not set");
            _authCredential.Token =
                config["Token"] ?? throw new ArgumentException("Token is not set");
            _authCredential.Secret =
                config["Secret"] ?? throw new ArgumentException("Secret is not set");

            var threadContentService = _services.GetRequiredService<IThreadContentService>();
            var content = await threadContentService.GetThreadContentAsync(threadId: 1821753);

            Assert.NotEmpty(content.Contents);
        }
    }
}
