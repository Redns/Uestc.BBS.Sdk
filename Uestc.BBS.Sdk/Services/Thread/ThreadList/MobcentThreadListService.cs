using System.Text.Json;
using Uestc.BBS.Sdk.Helpers;
using Uestc.BBS.Sdk.Services.Auth;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class MobcentThreadListService(HttpClient httpClient, AuthCredential credential)
        : IThreadListService
    {
        public async Task<IEnumerable<ThreadOverview>> GetThreadListAsync(
            string? route = null,
            uint page = 1,
            uint pageSize = 10,
            Board boardId = 0,
            uint moduleId = 2,
            uint typeId = 0,
            TopicSortType sortby = TopicSortType.New,
            TopicTopOrder topOrder = TopicTopOrder.WithoutTop,
            bool getPreviewSources = false,
            bool getPartialReply = false,
            CancellationToken cancellationToken = default
        )
        {
            var formData = new Dictionary<string, string>
            {
                { "r", string.IsNullOrEmpty(route) ? "forum/topiclist" : route },
                { "accessToken", credential.Token },
                { "accessSecret", credential.Secret },
                { nameof(page), page.ToString() },
                { nameof(pageSize), pageSize.ToString() },
                { nameof(boardId), boardId.ToInt32String() },
                { nameof(moduleId), moduleId.ToString() },
                { nameof(sortby), sortby.ToLowerString() },
                { nameof(topOrder), topOrder.ToInt32String() },
                { "circle", getPartialReply ? "1" : "0" },
                { "isImageList", getPreviewSources ? "1" : "0" },
            };

            // 测试发现获取全部主题时 typeId=0 是否提交不影响结果，但设置后服务器响应速度慢一倍
            if (typeId != 0)
            {
                formData["filterType"] = typeId.ToString();
            }

            using var resp = await httpClient.PostAsync(
                ApiEndpoints.GET_MOBILE_HOME_THREAD_LIST_URL,
                new FormUrlEncodedContent(formData),
                cancellationToken
            );
            resp.EnsureSuccessStatusCode();

            using var respStream = await resp.Content.ReadAsStreamAsync(cancellationToken);
            var threadList = await JsonSerializer.DeserializeAsync(
                respStream,
                MobcentThreadListRespContext.Default.MobcentThreadListResp,
                cancellationToken
            );

            return threadList?.Threads.Select(t => t.ToThreadOverview()) ?? [];
        }
    }
}
