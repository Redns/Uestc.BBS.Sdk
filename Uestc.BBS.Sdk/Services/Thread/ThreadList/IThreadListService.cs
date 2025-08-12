using FastEnumUtility;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadList
{
    public interface IThreadListService
    {
        Task<Thread[]> GetThreadListAsync(
            string? route = null,
            uint page = 1,
            uint pageSize = 10,
            uint moduleId = 2,
            Board boardId = 0,
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
