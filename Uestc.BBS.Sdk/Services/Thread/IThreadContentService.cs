namespace Uestc.BBS.Sdk.Services.Thread
{
    public interface IThreadContentService
    {
        Task<ThreadContent> GetThreadContentAsync(
            uint threadId,
            CancellationToken cancellationToken = default
        );
    }
}
