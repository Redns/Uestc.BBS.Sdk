using System.Drawing;
using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.JsonConverters;
using Uestc.BBS.Sdk.Models;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public class WebThreadOverview
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("thread_id")]
        public uint Id { get; set; }

        /// <summary>
        /// 板块
        /// </summary>
        [JsonPropertyName("forum_id")]
        public Board Board { get; set; }

        /// <summary>
        /// 子分类 ID
        /// </summary>
        [JsonPropertyName("type_id")]
        public uint TypeId { get; set; }

        /// <summary>
        /// Uid
        /// </summary>
        [JsonPropertyName("author_id")]
        public uint Uid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [JsonPropertyName("author")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        [JsonPropertyName("subject")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        [JsonPropertyName("summary")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 附件
        /// </summary>
        [JsonPropertyName("summary_attachments")]
        public Attachment[] Attachments { get; set; } = [];

        /// <summary>
        /// 热门帖子为发表时间，其余帖子为最新回复时间
        /// </summary>
        [JsonPropertyName("dateline")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 最后回复时间
        /// </summary>
        [JsonPropertyName("last_post")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime LastReplyDateTime { get; set; }

        /// <summary>
        /// 最后回复用户
        /// </summary>
        [JsonPropertyName("last_poster")]
        public string LastReplyUsername { get; set; } = string.Empty;

        /// <summary>
        /// 浏览量
        /// </summary>
        [JsonPropertyName("views")]
        public uint ViewCount { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        [JsonPropertyName("replies")]
        public uint ReplyCount { get; set; }

        /// <summary>
        /// 显示顺序（数值大的优先级高）
        /// </summary>
        [JsonPropertyName("display_order")]
        public uint DisplayOrder { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("is_rated")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsRated { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public uint Special { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public uint Attachment { get; set; }

        /// <summary>
        /// 是否被有形的大手操作过（移动/……）
        /// </summary>
        [JsonPropertyName("is_moderated")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsModerated { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("has_stick_reply")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasStickReply { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        [JsonPropertyName("is_closed")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsClosed { get; set; }

        /// <summary>
        /// 点赞+点踩
        /// </summary>
        [JsonPropertyName("recommends")]
        public int RecommendCount { get; set; }

        /// <summary>
        /// 点赞量
        /// </summary>
        [JsonPropertyName("recommend_add")]
        public uint LikeCount { get; set; }

        /// <summary>
        /// 点踩量
        /// </summary>
        [JsonPropertyName("recommend_sub")]
        public uint DislikeCount { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        [JsonPropertyName("favorite_times")]
        public uint FavoriteCount { get; set; }

        /// <summary>
        /// 分享量
        /// </summary>
        [JsonPropertyName("share_times")]
        public uint ShareCount { get; set; }

        /// <summary>
        /// 热度
        /// </summary>
        [JsonPropertyName("heats")]
        public uint Heat { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Stamp { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Icon { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Cover { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("max_position")]
        public uint MaxPosition { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public uint Comments { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("highlight_color")]
        [JsonConverter(typeof(String2ColorConverter))]
        public Color HightlightColor { get; set; } = Color.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("highlight_bold")]
        public bool HighlightBold { get; set; }

        public ThreadOverview ToThreadOverview(Uri baseUri) =>
            new()
            {
                Id = Id,
                Board = Board,
                BoardName = Board.GetLabel() ?? string.Empty,
                Title = Title,
                Subject = Subject,
                DateTime = DateTime,
                PreviewImageSources =
                [
                    .. Attachments
                        .Where(a => a.IsImage)
                        .Select(a => new Uri(baseUri, a.ThuhumbnailUrl))
                        .Select(u => u.AbsoluteUri),
                ],
                ViewCount = ViewCount,
                ReplyCount = ReplyCount,
                LikeCount = LikeCount,
                DislikeCount = DislikeCount,
                Uid = Uid,
                Username = Username,
                UserAvatar = new Uri(
                    baseUri,
                    $"uc_server/avatar.php?uid={Uid}&size=middle"
                ).AbsoluteUri,
                HasVote = false, // TODO 获取是否存在投票
            };
    }
}
