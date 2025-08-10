using System.Drawing;
using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Forum
{
    /// <summary>
    /// 公告
    /// </summary>
    public class Announcement
    {
        /// <summary>
        /// 类型
        /// </summary>
        public AnnouncementType Kind { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// 链接
        /// </summary>
        public string Href { get; set; } = string.Empty;

        /// <summary>
        /// 高亮颜色
        /// </summary>
        [JsonPropertyName("hightlight_color")]
        [JsonConverter(typeof(String2ColorConverter))]
        public Color HightlightColor { get; set; } = Color.Empty;

        /// <summary>
        /// 深色模式高亮颜色
        /// </summary>
        [JsonPropertyName("dark_hightlight_color")]
        [JsonConverter(typeof(String2ColorConverter))]
        public Color DarkHighlightColor { get; set; } = Color.Empty;
    }

    public enum AnnouncementType
    {
        Defaut = 1,
    }
}
