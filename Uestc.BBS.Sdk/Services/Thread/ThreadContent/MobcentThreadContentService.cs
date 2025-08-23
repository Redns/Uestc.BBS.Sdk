using System.Text.Json;
using Uestc.BBS.Sdk.Services.Auth;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadContent
{
    public class MobcentThreadContentService(HttpClient httpClient, AuthCredential credential)
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
            using var resp = await httpClient.PostAsync(
                ApiEndpoints.GET_MOBILE_THREAD_CONTENT_URL,
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "accessToken", credential.Token },
                        { "accessSecret", credential.Secret },
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

            return threadContent?.ToThreadContent() ?? throw new NullReferenceException();
        }
    }
}
