using System.Drawing;

namespace Uestc.BBS.Sdk.Services.System
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> GetAnnouncementsAsync(
            CancellationToken cancellationToken = default
        );
    }

    /// <summary>
    /// 公告
    /// </summary>
    public class Announcement
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 公告类型
        /// </summary>
        public AnnouncementType Type { get; set; } = AnnouncementType.Default;

        /// <summary>
        /// 网页链接
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 标题颜色
        /// </summary>
        public Color? TitleColor { get; set; }

        /// <summary>
        /// 标题深色颜色
        /// </summary>
        public Color? TitleDarkColor { get; set; }
    }

    /// <summary>
    /// 公告类型
    /// </summary>
    public enum AnnouncementType
    {
        Default = 1, // 默认
    }
}
