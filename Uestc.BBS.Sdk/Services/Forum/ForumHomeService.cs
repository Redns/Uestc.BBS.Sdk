using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Forum
{
    /// <summary>
    /// 论坛首页服务
    /// </summary>
    /// <param name="httpClientFactory">请使用 <see cref="ServiceExtensions.UseMobcentServices"/> 注入</param>
    public class ForumHomeService(IHttpClientFactory httpClientFactory) : IForumHomeService
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
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.MOBCENT_API);
            using var respStream =
                await httpClient.GetStreamAsync(ApiEndpoints.FORUM_HOME_URL, cancellationToken)
                ?? throw new NullReferenceException("Forum home data is null.");

            return await JsonSerializer.DeserializeAsync(
                    respStream,
                    ForumHomeDataContext.Default.ApiRespBaseForumHomeData,
                    cancellationToken
                ) ?? throw new NullReferenceException("Forum home data is null.");
        }
    }
}
