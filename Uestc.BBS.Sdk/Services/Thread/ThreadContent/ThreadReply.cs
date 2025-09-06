namespace Uestc.BBS.Sdk.Services.Thread.ThreadContent
{
    public class ThreadReply
    {
        /// <summary>
        /// ID
        /// </summary>
        public required uint Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public required DateTime CreateTime { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public required uint LikeCount { get; set; }

        /// <summary>
        /// 点踩数
        /// </summary>
        public required uint DislikeCount { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public required uint Position { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public required RichTextContent[] Contents { get; set; } = [];

        /// <summary>
        /// 是否置顶
        /// </summary>
        public required bool IsPinned { get; set; }

        /// <summary>
        /// 是否是楼主
        /// </summary>
        public required bool IsFromThreadMaster { get; set; }

        /// <summary>
        /// 用户 ID
        /// </summary>
        public required uint Uid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public required string Username { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        public required string UserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// 用户等级
        /// </summary>
        public required uint UserLevel { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        public required string UserGroup { get; set; } = string.Empty;

        /// <summary>
        /// 是否有引用
        /// </summary>
        public required bool HasQuote { get; set; }

        /// <summary>
        /// 引用的主题 ID
        /// </summary>
        public uint QuoteId { get; set; }

        /// <summary>
        /// 引用的主题用户名
        /// </summary>
        public string QuoteUsername { get; set; } = string.Empty;

        /// <summary>
        /// 引用的主题用户头像
        /// </summary>
        public string QuoteUserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// 引用的主题内容
        /// </summary>
        public string QuoteContent { get; set; } = string.Empty;
    }
}
