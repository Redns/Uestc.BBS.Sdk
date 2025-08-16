using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Forum
{
    /// <summary>
    /// 论坛首页服务
    /// </summary>
    /// <param name="client">请使用 <see cref="ServiceExtensions.AddForumHomeClient"/> 注入</param>
    public class ForumHomeService(HttpClient client) : IForumHomeService
    {
        /// <summary>
        /// 获取论坛首页数据（统计信息、公告）
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<ApiRespBase<ForumHomeData>> GetForumHomeDataAsync(
            CancellationToken cancellationToken
        )
        {
            using var respStream =
                await client.GetStreamAsync(ApiEndpoints.FORUM_HOME_URL, cancellationToken)
                ?? throw new NullReferenceException("Forum home data is null.");

            return await JsonSerializer.DeserializeAsync(
                    respStream,
                    ForumHomeDataContext.Default.ApiRespBaseForumHomeData,
                    cancellationToken
                ) ?? throw new NullReferenceException("Forum home data is null.");
        }
    }
}
