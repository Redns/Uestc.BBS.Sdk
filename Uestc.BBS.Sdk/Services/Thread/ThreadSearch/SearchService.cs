using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadSearch
{
    public class SearchService(HttpClient client) : ISearchService
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

            using var resp = await client
                .GetAsync($"?q={keyword}&page={page}", cancellationToken)
                .ContinueWith(t => t.Result.EnsureSuccessStatusCode(), cancellationToken);
            return JsonSerializer
                    .Deserialize(
                        await resp.Content.ReadAsStreamAsync(cancellationToken),
                        ThreadSearchResultContext.Default.ApiRespBaseThreadSearchResult
                    )
                    ?.Data ?? throw new Exception("Failed to deserialize search result");
        }
    }
}
