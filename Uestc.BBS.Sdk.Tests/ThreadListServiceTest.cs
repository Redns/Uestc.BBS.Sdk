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
                .AddWebAuthService(services => new Uri(BASRE_URL));
            collection.AddWebThreadListService(services => new Uri(BASRE_URL));

            _services = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task GetThreadListAsyncTest()
        {
            var config = _services.GetpartialService<IConfigurationRoot>();
            var authService = _services.GetpartialService<IAuthService>();
            var credential = _services.GetpartialService<AuthCredential>();

            _authCredential.Username =
                config["Username"] ?? throw new ArgumentException("Username is not set");
            _authCredential.Password =
                config["Password"] ?? throw new ArgumentException("Password is not set");

            // 获取 Cookie 和 Authorization
            await authService.LoginAsync(credential);

            Assert.NotEmpty(credential.Cookies);
            Assert.NotEmpty(credential.Authorization);

            // 获取帖子列表
            var threadListService = _services.GetpartialService<IThreadListService>();

            var threads = await threadListService.GetThreadListAsync(
                boardId: Board.EmploymentAndEntrepreneurship,
                sortby: TopicSortType.Essence
            );

            Assert.NotEmpty(threads);
        }
    }
}
