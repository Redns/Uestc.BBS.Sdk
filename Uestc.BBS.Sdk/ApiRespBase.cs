using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.Services.User;

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
        public User? User { get; set; }

        /// <summary>
        /// 系统信息
        /// </summary>
        public ApiStatus? System { get; set; }
    }

    /// <summary>
    /// API 状态
    /// </summary>
    public class ApiStatus
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
