using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.Models;

namespace Uestc.BBS.Sdk.Services.Forum
{
    /// <summary>
    /// 论坛首页服务
    /// </summary>
    public interface IForumHomeService
    {
        /// <summary>
        /// 获取论坛首页数据（统计信息、公告）
        /// </summary>
        /// <returns></returns>
        Task<ApiRespBase<ForumHomeData>> GetForumHomeDataAsync(
            CancellationToken cancellationToken = default
        );
    }

    public class ForumHomeData
    {
        [JsonPropertyName("global_stat")]
        public ForumStatistics ForumStatistics { get; set; }

        [JsonPropertyName("announcement")]
        public Announcement[] Announcements { get; set; } = [];
    }

    [JsonSerializable(typeof(ApiRespBase<ForumHomeData>))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class ForumHomeDataContext : JsonSerializerContext { }
}
