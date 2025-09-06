using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadContent
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="httpClientFactory">请使用 <see cref="ServiceExtensions.UseMobcentServices"/> 注入</param>
    public class MobcentThreadContentService(IHttpClientFactory httpClientFactory)
        : IThreadContentService
    {
        public async Task<ThreadContent> GetThreadContentAsync(
            uint threadId,
            uint authorId = 0,
            uint page = 0,
            uint pageSize = 30,
            bool reverseOrder = false,
            CancellationToken cancellationToken = default
        )
        {
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.MOBCENT_API);

            using var resp = await httpClient.PostAsync(
                ApiEndpoints.GET_MOBILE_THREAD_CONTENT_URL,
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "r", "forum/postlist" },
                        { "topicId", threadId.ToString() },
                        { "authorId", authorId.ToString() },
                        { "page", page.ToString() },
                        { "pageSize", pageSize.ToString() },
                        { "order", reverseOrder ? "1" : "0" },
                    }
                ),
                cancellationToken
            );
            resp.EnsureSuccessStatusCode();

            using var respStream = await resp.Content.ReadAsStreamAsync(cancellationToken);
            var threadContent = await JsonSerializer.DeserializeAsync(
                respStream,
                MobcentThreadContentRespContext.Default.MobcentThreadContentResp,
                cancellationToken
            );

            if (threadContent?.Head.ErrorCode is MobcentApiErrorCode.Unauthenticated)
            {
                throw new UnauthorizedAccessException("Invalid access token/secret");
            }

            return threadContent?.ToThreadContent()
                ?? throw new NullReferenceException("Thread content is null");
        }
    }
}
