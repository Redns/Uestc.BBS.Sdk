using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class ThreadListResp : ApiRespBase<ThreadListData> { }

    public class ThreadListData
    {
        /// <summary>
        /// Page
        /// </summary>
        public uint Page { get; set; }

        /// <summary>
        /// Page Size
        /// </summary>
        [JsonPropertyName("page_size")]
        public uint PageSize { get; set; }

        /// <summary>
        /// 主题总数
        /// </summary>
        [JsonPropertyName("total")]
        public uint TotalCount { get; set; }

        /// <summary>
        /// 主题列表
        /// </summary>
        [JsonPropertyName("rows")]
        public Thread[] Threads { get; set; } = [];
    }

    [JsonSerializable(typeof(ThreadListResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class ThreadListRespContext : JsonSerializerContext { }
}
