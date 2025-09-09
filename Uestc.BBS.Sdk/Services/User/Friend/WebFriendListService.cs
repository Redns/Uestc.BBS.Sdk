using System.Text.RegularExpressions;
using Uestc.BBS.Sdk.Services.Auth;

namespace Uestc.BBS.Sdk.Services.User.Friend
{
    public partial class WebFriendListService(IHttpClientFactory httpClientFactory)
        : IFriendListService
    {
        private readonly Regex _friendOverviewRegex = FriendOverviewRegex();

        public async Task<IEnumerable<BlacklistUser>> GetFriendListAsync(
            FriendType friendType,
            uint page = 1,
            CancellationToken cancellationToken = default
        )
        {
            var query =
                ApiEndpoints.USER_FRIEND_LIST_URL
                + "?mod=space&do=friend&page="
                + page
                + '&'
                + friendType switch
                {
                    FriendType.Friend => string.Empty,
                    FriendType.OnlineFriend => "view=online&type=friend",
                    FriendType.OnlineMember => "view=online&type=member",
                    FriendType.NearMember => "view=online&type=near",
                    FriendType.Visitor => "view=visitor",
                    FriendType.Trace => "view=trace",
                    FriendType.Blacklist => "view=blacklist",
                    _ => throw new ArgumentException("Unknown friend type"),
                };
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);

            using var resp = await httpClient.PostAsync(query, null, cancellationToken);
            resp.EnsureSuccessStatusCode();
            var respContent = await resp.Content.ReadAsStringAsync(cancellationToken);

            return _friendOverviewRegex
                .Matches(respContent)
                .Select(m => new BlacklistUser
                {
                    Uid = uint.Parse(m.Groups["uid"].Value),
                    Username = m.Groups["name"].Value,
                });
        }

        [GeneratedRegex(
            @"<a\s+href=""https://bbs\.uestcer\.org/home\.php\?mod=space&amp;uid=(?<uid>\d+)"">(?<name>[^<]+)</a>",
            RegexOptions.IgnoreCase | RegexOptions.Compiled,
            "zh-CN"
        )]
        private static partial Regex FriendOverviewRegex();
    }
}
