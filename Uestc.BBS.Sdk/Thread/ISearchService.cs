using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Thread
{
    public interface ISearchService
    {
        Task<ThreadSearchResult> SearchThreadsAsync(
            string keyword,
            uint pageIndex = 1,
            uint pageSize = 20,
            CancellationToken cancellationToken = default
        );
    }

    /// <summary>
    /// 主题搜索结果
    /// </summary>
    public class ThreadSearchResult
    {
        /// <summary>
        /// 主题总数
        /// </summary>
        [JsonPropertyName("total")]
        public uint TotalCount { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        public uint PageSize { get; set; }

        /// <summary>
        /// 当前分页索引
        /// </summary>
        public uint Page { get; set; }

        /// <summary>
        /// 主题列表
        /// </summary>
        [JsonPropertyName("rows")]
        public Thread[] Threads { get; set; } = [];
    }

    [JsonSerializable(typeof(ApiRespBase<ThreadSearchResult>))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class ThreadSearchResultContext : JsonSerializerContext { }
}
