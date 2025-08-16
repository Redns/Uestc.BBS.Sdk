namespace Uestc.BBS.Sdk.Services.Thread
{
    public class ThreadContent
    {
        /// <summary>
        /// ID
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// 版块
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// 最新回复时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 点赞量
        /// </summary>
        public uint LikeCount { get; set; }

        /// <summary>
        /// 点踩量
        /// </summary>
        public uint DislikeCount { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        public uint FavoriteCount { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Uid
        /// </summary>
        public uint Uid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// 用户等级
        /// </summary>
        public uint UserLevel { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        public string UserGroup { get; set; } = string.Empty;

        /// <summary>
        /// 用户签名
        /// </summary>
        public string UserSignature { get; set; } = string.Empty;

        /// <summary>
        /// 主题内容
        /// </summary>
        public RichTextContent[] Contents { get; set; } = [];
    }
}
