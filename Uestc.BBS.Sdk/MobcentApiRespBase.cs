using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk
{
    /// <summary>
    /// Mobcent API 响应基类
    /// </summary>
    public class MobcentApiRespBase
    {
        public int Rs { get; set; } = 0;

        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonIgnore]
        public bool Success => Rs is 1;

        public string ErrCode { get; set; } = string.Empty;

        public ApiRespBaseHeader Head { get; set; } = new();

        public ApiRespBaseBody Body { get; set; } = new();
    }

    public class ApiRespBaseHeader
    {
        public int Alert { get; set; } = 0;

        [JsonPropertyName("errCode")]
        public string ErrorCode { get; set; } = string.Empty;

        [JsonPropertyName("errInfo")]
        public string ErrorInformation { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;
    }

    public class ApiRespBaseBody
    {
        public ApiRespBaseBodyExternInfo ExternInfo { get; set; } = new();
    }

    public class ApiRespBaseBodyExternInfo
    {
        public string Padding { get; set; } = string.Empty;
    }

    [JsonSerializable(typeof(MobcentApiRespBase))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    )]
    public partial class ApiRespBaseContext : JsonSerializerContext { }
}
