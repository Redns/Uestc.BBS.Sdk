using System.Net;
using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;

namespace Uestc.BBS.Sdk.Auth
{
    public class AuthCredential
    {
        public uint Uid { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;

        public string Autherization { get; set; } = string.Empty;

        [JsonConverter(typeof(CookieContainerConverter))]
        public CookieContainer Cookies { get; set; } = new CookieContainer();
    }

    [JsonSerializable(typeof(AuthCredential))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class AuthCredentialContext : JsonSerializerContext { }
}
