using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Services.System
{
    public class WebGlobalStatusService(IHttpClientFactory httpClientFactory) : IGlobalStatusService
    {
        public async Task<GlobalStatus> GetGlobalStatusAsync(
            CancellationToken cancellationToken = default
        )
        {
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);
            using var resp = await httpClient.GetAsync(
                ApiEndpoints.GET_GLOBAL_STATUS,
                cancellationToken
            );
            resp.EnsureSuccessStatusCode();

            using var respContent = await resp.Content.ReadAsStreamAsync(cancellationToken);
            var result = await JsonSerializer.DeserializeAsync(
                respContent,
                WebGlobalStatusResponseContext.Default.ApiRespBaseWebGlobalStatusResponse,
                cancellationToken
            );

            return result?.Data?.Status?.ToGlobalStatus() ?? throw new NullReferenceException();
        }
    }

    public class WebGlobalStatusResponse
    {
        [JsonPropertyName("global_stat")]
        public WebGlobalStatus? Status { get; set; }
    }

    public class WebGlobalStatus
    {
        /// <summary>
        /// 今日发帖数
        /// </summary>
        [JsonPropertyName("today_posts")]
        public uint TodayPostCount { get; set; }

        /// <summary>
        /// 昨日发帖数
        /// </summary>
        [JsonPropertyName("yesterday_posts")]
        public uint YesterdayPostCount { get; set; }

        /// <summary>
        /// 总发帖数
        /// </summary>
        [JsonPropertyName("total_posts")]
        public uint TotalPostCount { get; set; }

        /// <summary>
        /// 总用户数
        /// </summary>
        [JsonPropertyName("total_users")]
        public uint TotalUserCount { get; set; }

        /// <summary>
        /// 新注册用户
        /// </summary>
        [JsonPropertyName("new_user")]
        public NewUserInfo? NewUser { get; set; }

        public GlobalStatus ToGlobalStatus() =>
            new()
            {
                TodayPostCount = TodayPostCount,
                YesterdayPostCount = YesterdayPostCount,
                TotalPostCount = TotalPostCount,
                TotalUserCount = TotalUserCount,
                NewUser = NewUser is not null
                    ? new NewUser { Uid = NewUser.Id, Username = NewUser.Username }
                    : null,
            };
    }

    public class NewUserInfo
    {
        [JsonPropertyName("uid")]
        public uint Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
    }

    [JsonSerializable(typeof(ApiRespBase<WebGlobalStatusResponse>))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class WebGlobalStatusResponseContext : JsonSerializerContext { }
}
