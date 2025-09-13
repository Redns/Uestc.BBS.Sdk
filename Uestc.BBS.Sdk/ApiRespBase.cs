using System.Text.Json.Serialization;
using FastEnumUtility;

namespace Uestc.BBS.Sdk
{
    public class ApiRespBase<T>
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public ErrorCode Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 数据
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo? User { get; set; }

        /// <summary>
        /// 系统信息
        /// </summary>
        public ApiInfo? System { get; set; }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户 ID
        /// </summary>
        public uint Uid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("new_pm")]
        public uint NewPm { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("new_pm_legacy")]
        public bool NewPmLegacy { get; set; }

        /// <summary>
        /// 新消息数量
        /// </summary>
        [JsonPropertyName("new_notification")]
        public uint NewNotificaionCount { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("new_grouppm_legacy")]
        public bool NewGrouppmLegacy { get; set; }
    }

    /// <summary>
    /// API 状态
    /// </summary>
    public class ApiInfo
    {
        [JsonPropertyName("settings_version")]
        public uint Version { get; set; }

        [JsonPropertyName("client_version")]
        public uint ClientVersion { get; set; }
    }

    /// <summary>
    /// 错误码
    /// </summary>
    public enum ErrorCode
    {
        [Label("成功")]
        Success = 0,

        [Label("身份认证失败")]
        AuthFailed = 1011,

        [Label("无权限访问")]
        NoAccess = 1014,
    }
}
