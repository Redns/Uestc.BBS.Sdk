namespace Uestc.BBS.Sdk.Services.Thread.ThreadReply
{
    public interface IThreadReplyService
    {
        Task<ThreadContent> GetThreadContentAsync(
            uint threadId,
            uint authorId = 0,
            uint page = 1,
            uint pageSize = 10,
            bool reverseOrder = false,
            CancellationToken cancellationToken = default
        );
    }
}
