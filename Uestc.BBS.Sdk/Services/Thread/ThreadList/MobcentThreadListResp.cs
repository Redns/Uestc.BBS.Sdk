using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class MobcentThreadListResp : MobcentApiRespBase
    {
        /// <summary>
        ///
        /// </summary>
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsOnlyTopicType { get; set; }

        /// <summary>
        /// 当前分页
        /// </summary>
        public uint Page { get; set; }

        /// <summary>
        /// 是否有下一分页
        /// </summary>
        public uint HasNext { get; set; }

        /// <summary>
        /// 总贴数
        /// </summary>
        public uint TotalNum { get; set; }

        /// <summary>
        /// 帖子摘要
        /// </summary>
        [JsonPropertyName("list")]
        public MobcentThreadOverview[] Threads { get; set; } = [];
    }

    [JsonSerializable(typeof(MobcentThreadListResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class MobcentThreadListRespContext : JsonSerializerContext { }
}
