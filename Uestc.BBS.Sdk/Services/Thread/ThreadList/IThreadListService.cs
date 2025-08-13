using FastEnumUtility;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public interface IThreadListService
    {
        /// <summary>
        /// 获取指定版块的帖子列表
        /// </summary>
        /// <param name="route"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="boardId">板块 ID</param>
        /// <param name="moduleId"></param>
        /// <param name="typeId">板块子分类 ID，为 0 时获取板块下所有主题</param>
        /// <param name="sortby">最新发表/最新回复/精华</param>
        /// <param name="topOrder">置顶帖设置</param>
        /// <param name="getPreviewSources">获取预览图像</param>
        /// <param name="getPartialReply">获取部分回复</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ThreadOverview>> GetThreadListAsync(
            string? route = null,
            uint page = 1,
            uint pageSize = 10,
            Board boardId = 0,
            uint moduleId = 2,
            uint typeId = 0,
            TopicSortType sortby = TopicSortType.New,
            TopicTopOrder topOrder = TopicTopOrder.WithoutTop,
            bool getPreviewSources = false,
            bool getPartialReply = false,
            CancellationToken cancellationToken = default
        );
    }

    /// <summary>
    /// 帖子排序方式
    /// </summary>
    public enum TopicSortType
    {
        [Label("最新")]
        New = 0,

        [Label("精华")]
        Essence,

        [Label("全部")]
        All,
    }

    /// <summary>
    /// 帖子置顶配置
    /// </summary>
    public enum TopicTopOrder
    {
        [Label("不返回置顶帖")]
        WithoutTop = 0,

        [Label("返回本版置顶帖")]
        WithCurrentSectionTop,

        [Label("返回分类置顶帖")]
        WithCategorySectionTop,

        [Label("返回全局置顶帖")]
        WithGlobalTop,
    }
}
