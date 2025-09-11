using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;
using Uestc.BBS.Sdk.Services.User;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class MobcentThreadOverview
    {
        /// <summary>
        /// 帖子 ID
        /// </summary>
        public uint Id => TopicId is not 0 ? TopicId : SourceId;

        /// <summary>
        /// 板块 ID
        /// </summary>
        [JsonPropertyName("board_id")]
        public Board Board { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        [JsonPropertyName("board_name")]
        public string BoardName { get; set; } = string.Empty;

        /// <summary>
        /// 帖子 ID
        /// 热门主题请使用 SourceId，其余主题使用 TopicId
        /// </summary>
        [JsonPropertyName("topic_id")]
        public uint TopicId { get; set; }

        [JsonPropertyName("source_id")]
        public uint SourceId { get; set; }

        /// <summary>
        /// 帖子类型
        /// </summary>
        [JsonConverter(typeof(StringToTopicTypeConverter))]
        public ThreadType Type { get; set; } = ThreadType.Normal;

        /// <summary>
        /// 帖子标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 发帖用户 ID
        /// </summary>
        [JsonPropertyName("user_id")]
        public uint Uid { get; set; }

        /// <summary>
        /// 发帖用户名
        /// </summary>
        [JsonPropertyName("user_nick_name")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 发帖用户头像
        /// </summary>
        public string UserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// 是否匿名发帖
        /// </summary>
        [JsonIgnore]
        public bool IsAnonymous => Uid is 0;

        /// <summary>
        /// 最新日期（Unix 毫秒级时间戳）
        /// 热门帖子为发表时间，其余帖子为最新回复时间
        /// </summary>
        [JsonPropertyName("last_reply_date")]
        [JsonConverter(typeof(UnixTimestampStringToDateTimeConverter))]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 是否包含投票
        /// </summary>
        [JsonPropertyName("vote")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasVote { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS
        /// </summary>
        public uint Hot { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        [JsonPropertyName("hits")]
        public uint ViewCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        [JsonPropertyName("replies")]
        public uint ReplyCount { get; set; }

        /// <summary>
        /// 是否为精华贴
        /// </summary>
        [JsonPropertyName("essence")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsEssence { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [JsonPropertyName("top")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsTop { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 摘要（为什么两个字段不能合起来）
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// 主题内首张图片（原图）
        /// </summary>
        [JsonPropertyName("pic_path")]
        public string? PicPath { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS
        /// </summary>
        public string Ratio { get; set; } = string.Empty;

        /// <summary>
        /// 枚举性别类型
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 用户头衔
        /// </summary>
        public string UserTitle { get; set; } = string.Empty;

        /// <summary>
        /// 用户等级
        /// </summary>
        public uint UserLevel => UserTitle.GetUserLevel();

        /// <summary>
        /// 点赞数
        /// </summary>
        [JsonPropertyName("recommendadd")]
        public uint LikeCount { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS
        /// </summary>
        public int Special { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS
        /// </summary>
        public int IsHasRecommendAdd { get; set; }

        /// <summary>
        /// 预览图
        /// </summary>
        [JsonPropertyName("imageList")]
        public string[] PreviewImageUrls { get; set; } = [];

        /// <summary>
        /// 主题来源链接
        /// </summary>
        public string SourceWebUrl { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS
        /// 参照帖子 https://bbs.uestc.edu.cn/forum.php?mod=viewthread&tid=2351215
        /// </summary>
        [JsonPropertyName("verify")]
        public UserVerify[] Verifies { get; set; } = [];

        public ThreadOverview ToThreadOverview() =>
            new()
            {
                Id = Id,
                Board = Board,
                BoardName = BoardName,
                Title = Title,
                Subject = !string.IsNullOrEmpty(Subject) ? Subject : Summary,
                DateTime = DateTime,
                PreviewImageSources = PreviewImageUrls,
                ViewCount = ViewCount,
                ReplyCount = ReplyCount,
                LikeCount = LikeCount,
                Uid = Uid,
                Username = Username,
                UserAvatar = UserAvatar,
                UserGender = Gender,
                HasVote = HasVote,
            };
    }
}
