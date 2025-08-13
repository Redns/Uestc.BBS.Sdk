using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.Thread
{
    public class Reply
    {
        #region 回复内容

        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("reply_posts_id")]
        public uint Id { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public uint Position { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonPropertyName("reply_type")]
        [JsonConverter(typeof(StringToReplyTypeConverter))]
        public ReplyType Type { get; set; } = ReplyType.Normal;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonPropertyName("posts_date")]
        [JsonConverter(typeof(UnixTimestampStringToDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [JsonPropertyName("reply_content")]
        public RichTextContent[] Contents { get; set; } = [];

        #endregion

        #region 用户

        /// <summary>
        /// 用户 ID
        /// </summary>
        [JsonPropertyName("reply_id")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonPropertyName("reply_name")]
        public string UserNickName { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        [JsonPropertyName("icon")]
        public string UserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// 用户等级
        /// </summary>
        [JsonPropertyName("level")]
        public uint UserLevel { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        public string UserTitle { get; set; } = string.Empty;

        /// <summary>
        /// 移动端标识
        /// </summary>
        public string MobileSign { get; set; } = string.Empty;

        #endregion

        #region 引用

        /// <summary>
        /// 是否包含引用
        /// </summary>
        [JsonPropertyName("is_quote")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasQuote { get; set; }

        /// <summary>
        /// 被引用回复 ID
        /// </summary>
        [JsonPropertyName("quote_pid")]
        public uint QuoteId { get; set; }

        /// <summary>
        /// 被引用楼层发表时间
        /// </summary>
        [JsonPropertyName("quote_time")]
        [JsonConverter(typeof(UnixTimestampStringToDateTimeConverter))]
        public DateTime QuoteCreatedAt { get; set; }

        /// <summary>
        /// 被引用用户昵称
        /// </summary>
        [JsonPropertyName("quote_user_name")]
        public string QuoteUserName { get; set; } = string.Empty;

        /// <summary>
        /// 被引用回复内容
        /// </summary>
        [JsonPropertyName("quote_content_bare")]
        public string QuoteContent { get; set; } = string.Empty;

        #endregion

        public ExtraPanel ExtraPanel { get; set; } = new();
    }

    public class ExtraPanel
    {
        public string Title { get; set; } = string.Empty;

        public ExtarPanelType Type { get; set; }

        public string Action { get; set; } = string.Empty;

        public string RecommendAdd { get; set; } = string.Empty;

        [JsonPropertyName("extParams")]
        public ExtraParam ExtraParam { get; set; } = new();
    }

    public class ExtraParam
    {
        public uint RecommendAdd { get; set; }

        public bool IsHasRecommendAdd { get; set; }

        public string BeforeAction { get; set; } = string.Empty;
    }

    public enum ExtarPanelType
    {
        [Label("评分")]
        Rate,

        [Label("支持")]
        Support,
    }

    public enum ReplyType
    {
        Normal,
    }
}
