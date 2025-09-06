using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.Message
{
    public class Message
    {
        /// <summary>
        /// ID
        /// </summary>
        public required uint Id { get; set; }

        /// <summary>
        /// Uid
        /// </summary>
        public required uint Uid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonConverter(typeof(String2MessageTypeConverter))]
        public required MessageType Type { get; set; }

        /// <summary>
        /// 是否未读
        /// </summary>
        public required bool IsUnread { get; set; }

        /// <summary>
        ///
        /// </summary>
        public required uint AuthorUid { get; set; }

        /// <summary>
        ///
        /// </summary>
        public required string AuthorUsername { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        public required string Content { get; set; } = string.Empty;
    }

    public enum MessageType
    {
        [Label("聊天")]
        Chat = 0,

        [Label("回复")]
        Reply,

        [Label("点评")]
        Comment,

        [Label("@")]
        At,

        [Label("评分")]
        Rate,

        [Label("好友")]
        Friend,

        [Label("个人空间")]
        Space,

        [Label("任务")]
        Task,

        [Label("举报")]
        Report,

        [Label("系统")]
        System,

        [Label("公共消息")]
        Admin,

        [Label("应用提醒")]
        App,

        [Label("其他")]
        Other,
    }
}
