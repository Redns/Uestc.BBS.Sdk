using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.Services.User;

namespace Uestc.BBS.Sdk.Services.Auth
{
    /// <summary>
    /// 移动端登录认证响应
    /// </summary>
    public class MobcentAuthResp : MobcentApiRespBase
    {
        #region Authorization
        public int GroupId { get; set; } = 0;

        public int IsValidation { get; set; } = 0;

        public string Token { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;
        #endregion

        public int Score { get; set; } = 0;

        public uint Uid { get; set; } = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; } = 0;

        public string Mobile { get; set; } = string.Empty;

        #region UserTitle
        /// <summary>
        /// 用户头衔
        /// </summary>
        public string UserTitle { get; set; } = string.Empty;

        /// <summary>
        /// 用户等级
        /// </summary>
        [JsonIgnore]
        public uint UserTitleLevel => UserTitle.GetUserLevel();

        /// <summary>
        /// 用户等级别名（如：鲤鱼）
        /// </summary>
        [JsonIgnore]
        public string UserTitleAlias => UserTitle.GetUserGroup();
        #endregion

        public Credit[] CreditShowList { get; set; } = [];
    }

    public class Credit
    {
        public string Type { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int Data { get; set; } = 0;
    }

    [JsonSerializable(typeof(MobcentAuthResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class AuthRespContext : JsonSerializerContext { }
}
