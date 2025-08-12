using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Services.Auth
{
    public class Authorization
    {
        [JsonPropertyName("authorization")]
        public string Token { get; set; } = string.Empty;
    }

    public class AuthorizationResp : ApiRespBase<Authorization> { }

    [JsonSerializable(typeof(AuthorizationResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    )]
    public partial class AuthorizationRespContext : JsonSerializerContext { }
}
