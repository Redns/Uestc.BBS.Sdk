namespace Uestc.BBS.Sdk.Services.Thread.ThreadContent
{
    public class ThreadContent
    {
        /// <summary>
        /// ID
        /// </summary>
        public required uint Id { get; set; }

        /// <summary>
        /// 版块
        /// </summary>
        public required Board Board { get; set; }

        /// <summary>
        /// 最新回复时间
        /// </summary>
        public required DateTime CreateTime { get; set; }

        /// <summary>
        /// 点赞量
        /// </summary>
        public required uint LikeCount { get; set; }

        /// <summary>
        /// 点踩量
        /// </summary>
        public required uint DislikeCount { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        public required uint FavoriteCount { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public required string Title { get; set; } = string.Empty;

        /// <summary>
        /// Uid
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
        /// 用户签名
        /// </summary>
        public required string UserSignature { get; set; } = string.Empty;

        /// <summary>
        /// 主题内容
        /// </summary>
        public required RichTextContent[] Contents { get; set; } = [];

        /// <summary>
        /// 回复列表
        /// </summary>
        public ThreadReply[] Replies { get; set; } = [];
    }
}
