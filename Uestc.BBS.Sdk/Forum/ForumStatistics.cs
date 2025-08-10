using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Forum
{
    /// <summary>
    /// 论坛统计信息
    /// </summary>
    public class ForumStatistics
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
        public User? Freshuser { get; set; }
    }
}
