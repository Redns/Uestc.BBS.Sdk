using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Services.System;
using Uestc.BBS.Sdk.Services.Thread;
using Uestc.BBS.Sdk.Services.Thread.ThreadList;

namespace Uestc.BBS.Sdk.Tests
{
    public class DailySentenceTest
    {
        private readonly IServiceProvider _services;

        private const string BASRE_URL = "https://bbs.uestcer.org";

        public DailySentenceTest()
        {
            var collection = new ServiceCollection();

            collection.AddDailySentencesService(BASRE_URL);

            _services = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task GetThreadListAsyncTest()
        {
            var dailysentenceService = _services.GetRequiredService<IDailySentenceService>();
            var dailysentence = await dailysentenceService.GetDailySentenceAsync();

            Assert.NotEmpty(dailysentence);
        }
    }
}
