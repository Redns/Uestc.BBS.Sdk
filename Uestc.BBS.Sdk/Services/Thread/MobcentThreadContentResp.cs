using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.Thread
{
    public class MobcentThreadContentResp : MobcentApiRespBase
    {
        /// <summary>
        /// Page
        /// </summary>
        public uint Page { get; set; }

        /// <summary>
        /// 是否包含下一页回复
        /// </summary>
        [JsonPropertyName("has_next")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasNextPage { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        [JsonPropertyName("total_num")]
        public uint ReplyCount { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        [JsonPropertyName("topic")]
        public MobcentThreadContent? Content { get; set; }
    }

    [JsonSerializable(typeof(MobcentThreadContentResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class MobcentThreadContentRespContext : JsonSerializerContext { }
}
