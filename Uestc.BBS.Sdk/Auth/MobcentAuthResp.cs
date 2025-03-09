using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Auth
{
    public class MobcentAuthResp : MobcentApiRespBase
    {
        #region Authorization
        public int GroupId { get; set; } = 0;
        public int IsValidation { get; set; } = 0;

        public string Token { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;
        #endregion

        [JsonIgnore]
        public bool Success => Rs is 1;

        public int Score { get; set; } = 0;

        public uint Uid { get; set; } = 0;

        public string Username { get; set; } = string.Empty;

        public string Avatar { get; set; } = string.Empty;

        public int Gender { get; set; } = 0;

        public string Mobile { get; set; } = string.Empty;

        public string UserTitle { get; set; } = string.Empty;

        public Credit[] CreditShowList { get; set; } = [];
    }

    public class Credit
    {
        public string Type { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int Data { get; set; } = 0;
    }

    [JsonSerializable(typeof(MobcentAuthResp))]
    [JsonSourceGenerationOptions(WriteIndented = true, PropertyNameCaseInsensitive = true)]
    public partial class AuthRespContext : JsonSerializerContext { }
}
