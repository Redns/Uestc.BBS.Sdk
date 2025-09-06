using Uestc.BBS.Sdk.Models;

namespace Uestc.BBS.Sdk.Services.Thread.ThreadReply
{
    public interface IThreadReplyService
    {
        /// <summary>
        /// 发送回复
        /// </summary>
        /// <param name="message">回复内容（如：水水![头像.jpg](i:2636013)\n）</param>
        /// <param name="threadId">回复的主题 ID</param>
        /// <param name="attachments">附件</param>
        /// <param name="postId">回复的评论的 ID，如果是回复主题则为 null</param>
        /// <returns>本回复 ID</returns>
        Task<uint> SendAsync(
            string message,
            uint threadId,
            Attachment[]? attachments = null,
            uint? postId = null,
            CancellationToken cancellationToken = default
        );
    }
}
