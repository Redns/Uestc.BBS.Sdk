using System.Text.Json;
using Uestc.BBS.Sdk.Services.Auth;

namespace Uestc.BBS.Sdk.Services.Thread
{
    public class MobcentThreadContentService(HttpClient httpClient, AuthCredential credential)
        : IThreadContentService
    {
        public async Task<ThreadContent> GetThreadContentAsync(
            uint threadId,
            CancellationToken cancellationToken = default
        )
        {
            using var resp = await httpClient
                .PostAsync(
                    ApiEndpoints.GET_MOBILE_THREAD_CONTENT_URL,
                    new FormUrlEncodedContent(
                        new Dictionary<string, string>
                        {
                            { "accessToken", credential.Token },
                            { "accessSecret", credential.Secret },
                            { "r", "forum/postlist" },
                            { "topicId", threadId.ToString() },
                            { "pageSize", "0" },
                        }
                    ),
                    cancellationToken
                )
                .ContinueWith(t => t.Result.EnsureSuccessStatusCode());

            return JsonSerializer
                    .Deserialize(
                        await resp.Content.ReadAsStreamAsync(cancellationToken),
                        MobcentThreadContentRespContext.Default.MobcentThreadContentResp
                    )
                    ?.Content?.ToThreadContent() ?? throw new NullReferenceException();
        }
    }
}
