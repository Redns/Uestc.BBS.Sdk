using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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

    public static partial class UserExtension
    {
        /// <summary>
        /// 获取用户等级
        /// UserTitle 包括如下结构：
        /// 1.虾米 (Lv.2)
        /// 2.实习版主
        /// 3.水藻河泥 (Lv.0 禁言中…)
        /// </summary>
        /// <param name="userTitle"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static uint GetUserLevel(this string userTitle)
        {
            if (!userTitle.Contains('('))
            {
                return 0;
            }

            var match = UserLevelRegex().Match(userTitle);
            if (match.Success && uint.TryParse(match.Groups[1].Value, out uint level))
            {
                return level;
            }

            return 0;
        }

        /// <summary>
        /// 获取用户组别
        /// /// UserTitle 包括如下结构：
        /// 1.虾米 (Lv.2)
        /// 2.实习版主
        /// 3.水藻河泥 (Lv.0 禁言中…)
        /// </summary>
        /// <param name="userTitle"></param>
        /// <returns></returns>
        public static string GetUserGroup(this string userTitle)
        {
            if (!userTitle.Contains('('))
            {
                return userTitle;
            }

            if (userTitle.Contains("禁言中"))
            {
                return "禁言中";
            }

            return userTitle.Split('(').First().Trim();
        }

        [GeneratedRegex(@"Lv\.(\d+)")]
        private static partial Regex UserLevelRegex();
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
