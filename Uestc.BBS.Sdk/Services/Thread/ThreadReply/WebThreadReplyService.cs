using System.Text.Json;
using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;
using Uestc.BBS.Sdk.Models;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadReply
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory">请使用 <see cref="ServiceExtensions.UseWebServices"/> 注入</param>
    public class WebThreadReplyService(IHttpClientFactory httpClientFactory) : IThreadReplyService
    {
        public async Task<uint> SendAsync(
            string message,
            uint threadId,
            Attachment[]? attachments = null,
            uint? postId = null,
            CancellationToken cancellationToken = default
        )
        {
            var content = new WebThreadReplyRequestContent
            {
                ThreadId = threadId,
                PostId = postId,
                Message = message,
                Attachments = attachments,
            };
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);

            using var resp = await httpClient.PostAsync(
                ApiEndpoints.POST_THREAD_REPLY_URL,
                new StringContent(
                    JsonSerializer.Serialize(
                        content,
                        WebThreadReplyRequestContentContext.Default.WebThreadReplyRequestContent
                    )
                ),
                cancellationToken
            );
            resp.EnsureSuccessStatusCode();

            var ret = await JsonSerializer.DeserializeAsync(
                await resp.Content.ReadAsStreamAsync(cancellationToken),
                WebThreadReplyResponseContentContext
                    .Default
                    .ApiRespBaseWebThreadReplyResponseContent,
                cancellationToken
            );

            return ret?.Data?.PostId ?? throw new NullReferenceException();
        }
    }

    public class WebThreadReplyRequestContent
    {
        [JsonPropertyName("thread_id")]
        public required uint ThreadId { get; set; }

        [JsonPropertyName("post_id")]
        public uint? PostId { get; set; }

        public required string Message { get; set; } = string.Empty;

        [JsonConverter(typeof(UintToBoolConverter))]
        public bool Usesig { get; set; } = true;

        public Attachment[]? Attachments { get; set; }

        public ThreadRenderFormat Format { get; set; } = ThreadRenderFormat.Markdown;
    }

    public class WebThreadReplyResponseContent
    {
        [JsonPropertyName("post_id")]
        public uint PostId { get; set; }
    }

    [JsonSerializable(typeof(WebThreadReplyRequestContent))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class WebThreadReplyRequestContentContext : JsonSerializerContext { }

    [JsonSerializable(typeof(ApiRespBase<WebThreadReplyResponseContent>))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class WebThreadReplyResponseContentContext : JsonSerializerContext { }
}
