using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Services.Thread
{
    public class MobcentThreadContent
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("topic_id")]
        public uint Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 浏览量
        /// </summary>
        [JsonPropertyName("hits")]
        public uint ViewCount { get; set; }

        /// <summary>
        /// 回复量
        /// </summary>
        [JsonPropertyName("replies")]
        public uint ReplyCount { get; set; }

        /// <summary>
        /// 是否精华
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
        /// 是否关注发帖人
        /// </summary>
        [JsonPropertyName("isFollow")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsFollowed { get; set; }

        /// <summary>
        /// 是否包含投票
        /// </summary>
        [JsonPropertyName("vote")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasVote { get; set; }

        /// <summary>
        /// 是否收藏
        /// </summary>
        [JsonPropertyName("is_favor")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsFavorite { get; set; }

        [JsonPropertyName("create_date")]
        [JsonConverter(typeof(UnixTimestampStringToDateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Hot { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Special { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// Uid
        /// </summary>
        [JsonPropertyName("user_id")]
        public uint Uid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [JsonPropertyName("user_nick_name")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        [JsonPropertyName("icon")]
        public string UserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// XXX 注意此字段并非用户等级
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        /// 用户头衔（如：Lv.11 鲨鱼）
        /// </summary>
        [JsonPropertyName("userTitle")]
        public string UserTitle { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [JsonPropertyName("content")]
        public RichTextContent[] Contents { get; set; } = [];

        /// <summary>
        /// Web 链接
        /// </summary>
        [JsonPropertyName("forumTopicUrl")]
        public string WebUrl { get; set; } = string.Empty;
    }
}
