using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Thread
{
    /// <summary>
    /// 淘专辑
    /// </summary>
    public class TaoCollection
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("collection_id")]
        public uint Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonPropertyName("dateline")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [JsonPropertyName("last_update")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        [JsonPropertyName("last_visit")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime LastVisitTime { get; set; }

        /// <summary>
        /// 关注人数
        /// </summary>
        [JsonPropertyName("follows")]
        public uint FollowCount { get; set; }

        /// <summary>
        /// 主题帖数量
        /// </summary>
        [JsonPropertyName("threads")]
        public uint ThreadCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        [JsonPropertyName("comments")]
        public uint CommentCount { get; set; }

        /// <summary>
        /// 评分数量
        /// </summary>
        [JsonPropertyName("rates")]
        public uint RateCount { get; set; }

        /// <summary>
        /// 平均评分
        /// </summary>
        [JsonPropertyName("average_rate")]
        public float AverageRate { get; set; }

        /// <summary>
        /// 标签（通过英文逗号分隔），推荐使用 <see cref="Tags"/> 代替
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

        [JsonIgnore]
        public string[]? Tags
        {
            get => field ??= Keyword.Split(',');
        }

        /// <summary>
        /// 创建人 ID
        /// </summary>
        [JsonPropertyName("uid")]
        public uint AuthorId { get; set; }

        /// <summary>
        /// 创建人昵称
        /// </summary>
        [JsonPropertyName("username")]
        public string AuthorName { get; set; } = string.Empty;

        /// <summary>
        /// 最新主题帖
        /// </summary>
        [JsonPropertyName("latest_thread")]
        public TaoCollectionLatestThread? LatestThread { get; set; }
    }

    /// <summary>
    /// 淘专辑最新主题帖
    /// </summary>
    public class TaoCollectionLatestThread
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("thread_id")]
        public uint Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [JsonPropertyName("subject")]
        public string Titile { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonPropertyName("dateline")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 发帖人昵称
        /// </summary>
        [JsonPropertyName("lastpost_author")]
        public string AuthorName { get; set; } = string.Empty;
    }

    [JsonSerializable(typeof(TaoCollection))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class TaoCollectionContext : JsonSerializerContext { }

    [JsonSerializable(typeof(TaoCollectionLatestThread))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class TaoCollectionLatestThreadContext : JsonSerializerContext { }
}
