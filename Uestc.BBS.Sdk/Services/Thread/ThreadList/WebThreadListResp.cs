using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class WebThreadListResp : ApiRespBase<WebThreadListData> { }

    public class WebThreadListData
    {
        /// <summary>
        /// 主题数量
        /// </summary>
        [JsonPropertyName("total")]
        public uint TotalCount { get; set; }

        /// <summary>
        /// Page
        /// </summary>
        public uint Page { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        [JsonPropertyName("page_size")]
        public uint PageSize { get; set; }

        /// <summary>
        /// 主题列表
        /// </summary>
        [JsonPropertyName("rows")]
        public WebThreadOverview[] Threads { get; set; } = [];
    }

    [JsonSerializable(typeof(WebThreadListResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class WebThreadListRespContext : JsonSerializerContext { }
}
