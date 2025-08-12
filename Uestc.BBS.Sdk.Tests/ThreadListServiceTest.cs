using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Services.Auth;
using Uestc.BBS.Sdk.Services.Thread;
using Uestc.BBS.Sdk.Services.Thread.ThreadList;

namespace Uestc.BBS.Sdk.Tests
{
    public class ThreadListServiceTest
    {
        private readonly IServiceProvider _services;

        private readonly AuthCredential _authCredential = new();

        private const string BASRE_URL = "https://bbs.uestcer.org";

        public ThreadListServiceTest()
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
                .AddThreadListService(BASRE_URL);

            _services = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task GetThreadListAsyncTest()
        {
            var config = _services.GetRequiredService<IConfigurationRoot>();

            _authCredential.Username =
                config["Username"] ?? throw new ArgumentException("Username is not set");
            _authCredential.Password =
                config["Password"] ?? throw new ArgumentException("Password is not set");
            _authCredential.Cookie =
                config["Cookie"] ?? throw new ArgumentException("Cookie is not set");
            _authCredential.Authorization =
                config["Authorization"] ?? throw new ArgumentException("Authorization is not set");

            var threadListService = _services.GetRequiredService<IThreadListService>();

            var threads = await threadListService.GetThreadListAsync(
                boardId: Board.EmploymentAndEntrepreneurship
            );

            Assert.NotEmpty(threads);
        }
    }
}
