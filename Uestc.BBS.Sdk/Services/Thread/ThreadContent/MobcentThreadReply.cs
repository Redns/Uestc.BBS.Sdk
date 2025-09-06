using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.JsonConverters;
using Uestc.BBS.Sdk.Services.User;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadContent
{
    public class MobcentThreadReply
    {
        #region 回复内容

        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("reply_posts_id")]
        public uint Id { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// 是否置顶
        /// </summary>
        [JsonPropertyName("poststick")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsPostStick { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [JsonConverter(typeof(AnythingToUintConverter))]
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
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("reply_status")]
        public int ReplyStatus { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("role_num")]
        public int RoleNum { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public bool DelThread { get; set; }

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
        public uint Uid { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [JsonPropertyName("reply_name")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 用户性别
        /// </summary>
        [JsonPropertyName("gender")]
        public Gender UserGender { get; set; } = Gender.Unknown;

        /// <summary>
        /// 用户头像
        /// </summary>
        [JsonPropertyName("icon")]
        public string UserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// 用户头衔（如：鲤鱼 (Lv.7)）
        /// </summary>
        public string UserTitle { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("level")]
        public uint Level { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// 移动端标识（如：来自安卓客户端）
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
        [JsonConverter(typeof(AnythingToUintConverter))]
        public uint QuoteId { get; set; }

        /// <summary>
        /// 被引用楼层发表时间
        /// </summary>
        [JsonPropertyName("quote_time")]
        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime QuoteCreatedAt { get; set; }

        /// <summary>
        /// 被引用用户昵称
        /// </summary>
        [JsonPropertyName("quote_user_name")]
        public string QuoteUsername { get; set; } = string.Empty;

        /// <summary>
        /// 被引用回复内容
        /// </summary>
        [JsonPropertyName("quote_content_bare")]
        public string QuoteContent { get; set; } = string.Empty;

        #endregion

        /// <summary>
        /// 附加面板
        /// </summary>
        [JsonPropertyName("extraPanel")]
        public ExtraPanel[] Operations { get; set; } = [];

        public ThreadReply ToThreadReply(uint threadAuthorId) =>
            new()
            {
                Id = Id,
                CreateTime = CreateTime,
                LikeCount =
                    Operations
                        .FirstOrDefault(o => o.Type is ExtraPanelType.Support)
                        ?.ExtraParam.RecommendAdd ?? 0,
                DislikeCount = 0,
                Position = Position,
                IsPinned = IsPostStick,
                Contents = Contents,
                Uid = Uid,
                Username = Username,
                UserAvatar = UserAvatar,
                UserLevel = UserTitle.GetUserLevel(),
                UserGroup = UserTitle.GetUserGroup(),
                IsFromThreadMaster = Uid == threadAuthorId && Uid != 0,
                HasQuote = HasQuote,
                QuoteId = QuoteId,
                QuoteUsername = QuoteUsername,
                QuoteContent = QuoteContent,
            };
    }

    public class ExtraPanel
    {
        /// <summary>
        /// 标题（如：支持）
        /// </summary>
        public string Title { get; set; } = string.Empty;

        [JsonConverter(typeof(StringToExtraPanelTypeConverter))]
        public ExtraPanelType Type { get; set; }

        public string Action { get; set; } = string.Empty;

        public string RecommendAdd { get; set; } = string.Empty;

        [JsonPropertyName("extParams")]
        public ExtraParam ExtraParam { get; set; } = new();
    }

    public class ExtraParam
    {
        /// <summary>
        /// 点赞数
        /// </summary>
        public uint RecommendAdd { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsHasRecommendAdd { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public string BeforeAction { get; set; } = string.Empty;
    }

    public enum ExtraPanelType
    {
        [Label("评分")]
        Rate,

        [Label("支持")]
        Support,
    }

    public enum ReplyType
    {
        Normal = 0,

        Normal_Complex,
    }
}
