using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Services.System;

namespace Uestc.BBS.Sdk.Tests
{
    public class DailySentenceTest
    {
        private readonly IServiceProvider _services;

        private const string BASRE_URL = "https://bbs.uestcer.org";

        public DailySentenceTest()
        {
            var collection = new ServiceCollection();

            collection.AddDailySentencesService(services => new Uri(BASRE_URL));

            _services = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task GetDailySentenceAsyncTest()
        {
            var dailysentenceService = _services.GetRequiredService<IDailySentenceService>();
            var dailysentence = await dailysentenceService.GetDailySentenceAsync();

            Assert.NotEmpty(dailysentence);
        }
    }
}
