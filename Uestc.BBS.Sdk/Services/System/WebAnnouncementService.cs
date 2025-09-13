using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.System
{
    public class WebAnnouncementService(IHttpClientFactory httpClientFactory) : IAnnouncementService
    {
        public async Task<IEnumerable<Announcement>> GetAnnouncementsAsync(
            CancellationToken cancellationToken = default
        )
        {
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);
            using var resp = await httpClient.GetAsync(
                ApiEndpoints.GET_ANNOUNCEMENT_LIST_URL,
                cancellationToken
            );
            resp.EnsureSuccessStatusCode();

            using var respContent = await resp.Content.ReadAsStreamAsync(cancellationToken);
            var ret = await JsonSerializer.DeserializeAsync(
                respContent,
                WebAnnouncementResponseContext.Default.ApiRespBaseWebAnnouncementResponse,
                cancellationToken
            );

            return ret?.Data?.Announcements.Select(a => a.ToAnnouncement()) ?? [];
        }
    }

    public class WebAnnouncementResponse
    {
        /// <summary>
        /// 公告列表
        /// </summary>
        [JsonPropertyName("announcement")]
        public WebAnnouncement[] Announcements { get; set; } = [];
    }

    public class WebAnnouncement
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        [JsonPropertyName("summary")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 公告类型
        /// </summary>
        public WebAnnouncementKind Kind { get; set; } = WebAnnouncementKind.Default;

        /// <summary>
        /// 网页链接
        /// </summary>
        public string Href { get; set; } = string.Empty;

        /// <summary>
        /// 标题颜色
        /// </summary>
        [JsonPropertyName("highlight_color")]
        [JsonConverter(typeof(String2ColorConverter))]
        public Color? TitleColor { get; set; }

        /// <summary>
        /// 标题深色颜色
        /// </summary>
        [JsonPropertyName("dark_highlight_color")]
        [JsonConverter(typeof(String2ColorConverter))]
        public Color? TitleDarkColor { get; set; }

        public Announcement ToAnnouncement() =>
            new()
            {
                Title = Title,
                Subject = Subject,
                Type = Kind switch
                {
                    WebAnnouncementKind.Default => AnnouncementType.Default,
                    _ => throw new ArgumentException("Unknown announcement type"),
                },
                Url = Href,
                TitleColor = TitleColor,
                TitleDarkColor = TitleDarkColor,
            };
    }

    /// <summary>
    /// 公告类型
    /// </summary>
    public enum WebAnnouncementKind
    {
        Default = 1,
    }

    [JsonSerializable(typeof(ApiRespBase<WebAnnouncementResponse>))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class WebAnnouncementResponseContext : JsonSerializerContext { }
}
