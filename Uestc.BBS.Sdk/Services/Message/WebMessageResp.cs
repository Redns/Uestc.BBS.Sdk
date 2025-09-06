using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.Message
{
    public class WebMessageResp { }

    public class WebMessage
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
        ///
        /// </summary>
        [JsonPropertyName("author_id")]
        public uint AuthorUid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("author")]
        public string AuthorUsername { get; set; } = string.Empty;

        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonPropertyName("dateline")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [JsonPropertyName("html_message")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("from_id")]
        public uint FromId { get; set; }

        [JsonPropertyName("from_id_type")]
        public string FromIdType { get; set; } = string.Empty;

        [JsonPropertyName("from_num")]
        public int FromNum { get; set; }

        /// <summary>
        /// 好友申请备注（如：我是XXX）
        /// </summary>
        [JsonPropertyName("friend_note")]
        public string FriendNote { get; set; } = string.Empty;

        public int Category { get; set; }

        public string Kind { get; set; } = string.Empty;

        /// <summary>
        /// 主题帖 ID
        /// </summary>
        [JsonPropertyName("thread_id")]
        public uint ThreadId { get; set; }

        /// <summary>
        /// 回复 ID
        /// </summary>
        [JsonPropertyName("post_id")]
        public uint ReplyId { get; set; }

        /// <summary>
        /// 评分理由
        /// </summary>
        [JsonPropertyName("reason")]
        public string RateReason { get; set; } = string.Empty;
    }
}
