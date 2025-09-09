using System.Text.Json;
using Uestc.BBS.Sdk.Helpers;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class WebThreadListService(IHttpClientFactory httpClientFactory) : IThreadListService
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
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);

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
            using var resp = await httpClient.GetAsync(uriBuilder.Uri, cancellationToken);
            resp.EnsureSuccessStatusCode();

            using var respStream = await resp.Content.ReadAsStreamAsync(cancellationToken);
            var threadList = await JsonSerializer.DeserializeAsync(
                respStream,
                WebThreadListRespContext.Default.WebThreadListResp,
                cancellationToken
            );

            return threadList?.Data?.Threads.Select(t =>
                {
                    var threadOverview = t.ToThreadOverview(httpClient.BaseAddress!);
                    if (!getPreviewSources)
                    {
                        threadOverview.PreviewImageSources = [];
                    }
                    return threadOverview;
                }) ?? [];
        }
    }
}
