using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadSearch
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory">请使用 <see cref="ServiceExtensions.UseWebServices"/> 注入</param>
    public class SearchService(IHttpClientFactory httpClientFactory) : ISearchService
    {
        public async Task<ThreadSearchResult> SearchThreadsAsync(
            string keyword,
            uint page = 1,
            uint pageSize = 20,
            CancellationToken cancellationToken = default
        )
        {
            if (page < 1 || pageSize == 0)
            {
                throw new ArgumentException("Invalid page index or page size");
            }

            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);
            using var respStream =
                await httpClient.GetStreamAsync($"?q={keyword}&page={page}", cancellationToken)
                ?? throw new Exception("Failed to deserialize search result");

            var threadSearchResult = await JsonSerializer.DeserializeAsync(
                respStream,
                ThreadSearchResultContext.Default.ApiRespBaseThreadSearchResult,
                cancellationToken
            );

            return threadSearchResult?.Data
                ?? throw new Exception("Failed to deserialize search result");
        }
    }
}
