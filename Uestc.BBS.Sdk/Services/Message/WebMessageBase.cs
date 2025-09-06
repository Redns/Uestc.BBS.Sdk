using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.Message
{
    public class WebMessageBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Uid
        /// </summary>
        [JsonPropertyName("user_id")]
        public uint Uid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonConverter(typeof(String2MessageTypeConverter))]
        public MessageType Type { get; set; }

        /// <summary>
        /// 是否未读
        /// </summary>
        [JsonPropertyName("unread")]
        public bool IsUnread { get; set; }

        /// <summary>
        /// 消息来源用户 ID
        /// </summary>
        [JsonPropertyName("author_id")]
        public uint AuthorUid { get; set; }

        /// <summary>
        /// 消息来源用户名
        /// </summary>
        [JsonPropertyName("author")]
        public string AuthorUsername { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonPropertyName("dateline")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [JsonPropertyName("html_message")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Category { get; set; }
    }
}
