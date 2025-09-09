using Uestc.BBS.Sdk.Services.Auth;

namespace Uestc.BBS.Sdk.Services.User.Friend
{
    public interface IFriendListService
    {
        Task<IEnumerable<BlacklistUser>> GetFriendListAsync(
            FriendType friendType,
            uint page = 1,
            CancellationToken cancellationToken = default
        );
    }

    public enum FriendType
    {
        Friend = 0, // 所有好友
        OnlineFriend, // 在线好友
        OnlineMember, // 在线成员
        NearMember, // 附近成员
        Visitor, // 我的访客
        Trace, // 我的足迹
        Blacklist, // 黑名单
    }
}
