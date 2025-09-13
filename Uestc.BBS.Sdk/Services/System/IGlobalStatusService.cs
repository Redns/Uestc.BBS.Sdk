namespace Uestc.BBS.Sdk.Services.System
{
    public interface IGlobalStatusService
    {
        Task<GlobalStatus> GetGlobalStatusAsync(CancellationToken cancellationToken = default);
    }

    public class GlobalStatus
    {
        /// <summary>
        /// 今日发帖数
        /// </summary>
        public uint TodayPostCount { get; set; }

        /// <summary>
        /// 昨日发帖数
        /// </summary>
        public uint YesterdayPostCount { get; set; }

        /// <summary>
        /// 总发帖数
        /// </summary>
        public uint TotalPostCount { get; set; }

        /// <summary>
        /// 总用户数
        /// </summary>
        public uint TotalUserCount { get; set; }

        /// <summary>
        /// 新用户
        /// </summary>
        public NewUser? NewUser { get; set; }
    }

    public class NewUser
    {
        public uint Uid { get; set; }

        public string Username { get; set; } = string.Empty;
    }
}
