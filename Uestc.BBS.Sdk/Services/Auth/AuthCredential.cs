using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Services.Auth
{
    public class AuthCredential
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
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Token 和 Secret 用于移动端接口认证
        /// </summary>
        public string Token { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;

        /// <summary>
        /// Cookie 和 Autherization 用于网页端接口认证
        /// </summary>
        public string Cookie { get; set; } = string.Empty;

        public string Authorization { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 用户等级
        /// </summary>
        public uint Level { get; set; } = 1;

        /// <summary>
        /// 用户组
        /// </summary>
        public string Group { get; set; } = string.Empty;

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// 此处序列化用户 AutoCompleteBox 显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Username;
        }
    }

    [JsonSerializable(typeof(AuthCredential))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class AuthCredentialContext : JsonSerializerContext { }
}
