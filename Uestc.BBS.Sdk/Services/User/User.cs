using System.Text.Json.Serialization;
using FastEnumUtility;

namespace Uestc.BBS.Sdk.Services.User
{
    public class User
    {
        /// <summary>
        /// 用户 ID
        /// </summary>
        public uint Uid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("new_pm")]
        public uint NewPm { get; set; }

        [JsonPropertyName("new_pm_legacy")]
        public bool NewPmLegacy { get; set; }

        /// <summary>
        /// 新消息数量
        /// </summary>
        [JsonPropertyName("new_notification")]
        public uint NewNotificaionCount { get; set; }

        [JsonPropertyName("new_grouppm_legacy")]
        public bool NewGrouppmLegacy { get; set; }
    }

    /// <summary>
    /// WHAT'S THIS?
    /// https://bbs.uestc.edu.cn/forum.php?mod=viewthread&tid=2351215
    /// "verify": [
    ///  {
    ///    "icon": "https://bbs.uestc.edu.cn/data/attachment/common/c4/common_1_verify_icon.png",
    ///    "vid": 1,
    ///    "verifyName": "vip"
    ///  }
    ///]
    /// </summary>
    public class UserVerify
    {
        public int Vid { get; set; }

        [JsonPropertyName("verifyName")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string IconUrl { get; set; } = string.Empty;
    }

    public static class UserExtension
    {
        public static uint GetUserTitleLevel(this string userTitle)
        {
            if (uint.TryParse(userTitle.Split('.').Last()[0..^1], out var level))
            {
                return level;
            }

            throw new ArgumentException("Unable to parse level from user title", nameof(userTitle));
        }

        public static string GetUserTitleAlias(this string userTitle) =>
            userTitle.Split('(').First().Trim();
    }

    public enum Gender
    {
        [Label("未知")]
        Unknown = 0,

        [Label("男")]
        Male,

        [Label("女")]
        Female,
    }
}
