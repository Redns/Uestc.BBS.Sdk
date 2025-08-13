using System.Text.Json;
using Uestc.BBS.Sdk.Helpers;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class WebThreadListService(HttpClient httpClient) : IThreadListService
    {
        public async Task<IEnumerable<ThreadOverview>> GetThreadListAsync(
            string? route = null,
            uint page = 1,
            uint pageSize = 10,
            Board boardId = Board.Latest,
            uint moduleId = 2,
            uint typeId = 0,
            TopicSortType sortby = TopicSortType.New,
            TopicTopOrder topOrder = TopicTopOrder.WithoutTop,
            bool getPreviewSources = false,
            bool getPartialReply = false,
            CancellationToken cancellationToken = default
        )
        {
            sortby = sortby is TopicSortType.New ? TopicSortType.All : sortby;
            var uriBuilder = new UriBuilder(httpClient.BaseAddress!)
            {
                Path = ApiEndpoints.GET_THREAD_LIST_URL,
                Query = new Dictionary<string, string?>
                {
                    ["forum_id"] = boardId.ToInt32String(),
                    ["page"] = page.ToString(),
                    ["sort_by"] = sortby.ToInt32String(),
                    ["forum_details"] = "0",
                }.ToQueryString(),
            };
            using var resp = await httpClient
                .GetAsync(uriBuilder.Uri, cancellationToken)
                .ContinueWith(t => t.Result.EnsureSuccessStatusCode());

            return JsonSerializer
                    .Deserialize(
                        await resp.Content.ReadAsStreamAsync(cancellationToken),
                        WebThreadListRespContext.Default.WebThreadListResp
                    )
                    ?.Data?.Threads.Select(t => t.ToThreadOverview(httpClient.BaseAddress!)) ?? [];
        }
    }
}
