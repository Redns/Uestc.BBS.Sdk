using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk
{
    public class ApiRespBase<T>
    {
        public uint Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public T Data { get; set; }

        public User User { get; set; }

        public System System { get; set; }
    }

    public class User
    {
        public uint Uid { get; set; }

        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("new_pm")]
        public uint NewPm { get; set; }

        [JsonPropertyName("new_pm_legacy")]
        public bool NewPmLegacy { get; set; }

        [JsonPropertyName("new_notification")]
        public uint NewNotificaionCount { get; set; }

        [JsonPropertyName("new_grouppm_legacy")]
        public bool NewGrouppmLegacy { get; set; }
    }

    public class System
    {
        [JsonPropertyName("settings_version")]
        public uint Version { get; set; }

        [JsonPropertyName("client_version")]
        public uint ClientVersion { get; set; }
    }
}
