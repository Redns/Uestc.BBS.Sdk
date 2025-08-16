using System.Text.Json.Serialization;
using Uestc.BBS.Sdk.JsonConverters;
using Uestc.BBS.Sdk.Services.User;

namespace Uestc.BBS.Sdk.Services.Thread
{
    public class MobcentThreadContentResp : MobcentApiRespBase
    {
        /// <summary>
        /// Page
        /// </summary>
        public uint Page { get; set; }

        /// <summary>
        /// 是否包含下一页回复
        /// </summary>
        [JsonPropertyName("has_next")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasNextPage { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        [JsonPropertyName("total_num")]
        public uint ReplyCount { get; set; }

        /// <summary>
        /// 版块
        /// </summary>
        [JsonPropertyName("boardId")]
        public Board Board { get; set; }

        /// <summary>
        /// 版块名称
        /// </summary>
        [JsonPropertyName("forumName")]
        public string BoardName { get; set; } = string.Empty;

        /// <summary>
        /// 帖子内容
        /// </summary>
        [JsonPropertyName("topic")]
        public MobcentThreadContent? Content { get; set; }

        public ThreadContent? ToThreadContent() =>
            Content is not null
                ? new()
                {
                    Id = Content.Id,
                    Title = Content.Title,
                    Board = Board,
                    CreateTime = Content.CreateTime,
                    Uid = Content.Uid,
                    Username = Content.Username,
                    UserAvatar = Content.UserAvatar,
                    UserLevel = Content.UserTitle.GetUserTitleLevel(),
                    UserGroup = Content.UserTitle.GetUserTitleAlias(),
                    UserSignature = string.Empty,
                    Contents = Content.Contents,
                }
                : null;
    }

    [JsonSerializable(typeof(MobcentThreadContentResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class MobcentThreadContentRespContext : JsonSerializerContext { }
}
