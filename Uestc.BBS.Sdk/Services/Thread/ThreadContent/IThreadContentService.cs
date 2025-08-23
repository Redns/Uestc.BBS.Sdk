namespace Uestc.BBS.Sdk.Services.Thread.ThreadContent
{
    public interface IThreadContentService
    {
        Task<ThreadContent> GetThreadContentAsync(
            uint threadId,
            uint authorId = 0,
            uint page = 0,
            uint pageSize = 0,
            bool reverseOrder = false,
            CancellationToken cancellationToken = default
        );
    }
}
