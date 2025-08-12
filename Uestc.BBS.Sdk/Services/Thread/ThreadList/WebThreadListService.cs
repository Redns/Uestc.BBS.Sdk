using System.Text.Json;
using System.Web;
using Uestc.BBS.Sdk.Helpers;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    /// <summary>
    /// 网页端主题列表服务
    /// </summary>
    /// <param name="httpClient"></param>
    public class WebThreadListService(HttpClient httpClient) : IThreadListService
    {
        /// <summary>
        /// 获取主题列表
        /// </summary>
        /// <param name="route">无效</param>
        /// <param name="page"></param>
        /// <param name="pageSize">无效</param>
        /// <param name="moduleId">无效</param>
        /// <param name="boardId">主题板块</param>
        /// <param name="sortby">必须设置为 <see cref="TopicSortType.Essence"/>，否则请求返回 HTTP 400 Bad Request</param>
        /// <param name="topOrder">无效</param>
        /// <param name="getPreviewSources">无效</param>
        /// <param name="getPartialReply">无效</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Thread[]> GetThreadListAsync(
            string? route = null,
            uint page = 1,
            uint pageSize = 10,
            uint moduleId = 2,
            Board boardId = 0,
            TopicSortType sortby = TopicSortType.New,
            TopicTopOrder topOrder = TopicTopOrder.WithoutTop,
            bool getPreviewSources = false,
            bool getPartialReply = false,
            CancellationToken cancellationToken = default
        )
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["page"] = page.ToString();
            parameters["forum_id"] = boardId.ToInt32String();
            parameters["sort_by"] = "1";
            parameters["forum_details"] = "0";

            var uriBuilder = new UriBuilder()
            {
                Path = ApiEndpoints.GET_THREAD_LIST_URL,
                Query = parameters.ToString(),
            };
            using var respMsg = await httpClient
                .GetAsync(uriBuilder.Uri.PathAndQuery, cancellationToken)
                .ContinueWith(t => t.Result.EnsureSuccessStatusCode());
            var resp = JsonSerializer.Deserialize(
                await respMsg.Content.ReadAsStreamAsync(cancellationToken),
                ThreadListRespContext.Default.ThreadListResp
            );

            return resp?.Data?.Threads ?? [];
        }
    }
}
