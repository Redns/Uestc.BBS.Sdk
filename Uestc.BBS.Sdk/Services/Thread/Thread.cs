using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.JsonConverters;
using Uestc.BBS.Sdk.Models;

namespace Uestc.BBS.Sdk.Services.Thread
{
    /// <summary>
    /// 主题（帖子）
    /// </summary>
    public class Thread
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonPropertyName("thread_id")]
        public uint Id { get; set; }

        /// <summary>
        /// 版块
        /// </summary>
        [JsonPropertyName("forum_id")]
        public Board Board { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        [JsonPropertyName("type_id")]
        public uint Type { get; set; }

        /// <summary>
        /// Uid
        /// </summary>
        [JsonPropertyName("author_id")]
        public uint Uid { get; set; }

        /// <summary>
        /// 发帖人
        /// </summary>
        [JsonPropertyName("author")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        [JsonPropertyName("subject")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        [JsonPropertyName("summary")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonPropertyName("dateline")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后回复时间
        /// </summary>
        [JsonPropertyName("last_post")]
        [JsonConverter(typeof(UnixTimestamp2DateTimeConverter))]
        public DateTime LastPostTime { get; set; }

        /// <summary>
        /// 最后回复人
        /// </summary>
        [JsonPropertyName("last_poster")]
        public string LastPostAuthor { get; set; } = string.Empty;

        /// <summary>
        /// 浏览量
        /// </summary>
        public uint ViewCount { get; set; }

        /// <summary>
        /// 回复数
        /// </summary>
        public uint ReplyCount { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        [JsonPropertyName("recommend_add")]
        public uint LikeCount { get; set; }

        /// <summary>
        /// 点踩数
        /// </summary>
        [JsonPropertyName("recommend_sub")]
        public uint DislikeCount { get; set; }

        /// <summary>
        /// 收藏数
        /// </summary>
        [JsonPropertyName("favorite_times")]
        public uint FavoriteCount { get; set; }

        /// <summary>
        /// 分享数
        /// </summary>
        [JsonPropertyName("share_times")]
        public uint ShareCount { get; set; }

        /// <summary>
        /// 点评数（包含对主题和回复的点评）
        /// </summary>
        [JsonPropertyName("comments")]
        public uint CommentCount { get; set; }

        /// <summary>
        /// 是否可回复
        /// </summary>
        [JsonPropertyName("can_reply")]
        public uint CanReply { get; set; }

        /// <summary>
        /// 显示顺序，大于 0 时表示置顶
        /// </summary>
        public uint DisplayOrder { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        [JsonIgnore]
        public bool IsTop => DisplayOrder > 0;

        /// <summary>
        /// 是否被加水
        /// </summary>
        [JsonPropertyName("is_rated")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsRated { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public uint Special { get; set; }

        /// <summary>
        /// 是否被有形的大手操作过（移动/合并/分类/删除回复/……）
        /// 例如：https://bbs.uestc.edu.cn/thread/2264922 本主题由 ____ 于 星期一 20:56 合并
        /// </summary>
        [JsonPropertyName("is_moderated")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsModerated { get; set; }

        /// <summary>
        /// 是否有置顶回复
        /// </summary>
        [JsonPropertyName("has_stick_reply")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasStickReply { get; set; }

        /// <summary>
        /// 是否关闭
        /// </summary>
        [JsonPropertyName("is_closed")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool IsClosed { get; set; }

        /// <summary>
        /// 热度
        /// </summary>
        public uint Heats { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Stamp { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public int Icon { get; set; }

        /// <summary>
        /// TODO WHAT'S THIS?
        /// </summary>
        public uint Cover { get; set; }

        /// <summary>
        /// 是否有附件（注意：此字段含义没有文档解释，笔者测试时发现包含附件时值为 2，不包含时值为 0）
        /// </summary>
        [JsonPropertyName("attachment")]
        [JsonConverter(typeof(UintToBoolConverter))]
        public bool HasAttachment { get; set; }

        /// <summary>
        /// 主题附件
        /// </summary>
        [JsonPropertyName("summary_attachments")]
        public Attachment[] Attachments { get; set; } = [];

        /// <summary>
        /// 淘专辑列表
        /// </summary>
        [JsonPropertyName("collections")]
        public TaoCollection[]? TaoCollections { get; set; }
    }

    /// <summary>
    /// 版块 ID 枚举
    /// </summary>
    public enum Board
    {
        [Label("站务公告")]
        StationAnnouncement = 2,

        [Label("同城同乡")]
        CityTownship = 17,

        [Label("学术交流")]
        AcademicExchange = 20,

        [Label("水手之家")]
        WaterHouse = 25,

        [Label("情感专区")]
        Emotional = 45,

        [Label("站务综合")]
        StationComprehensive = 46,

        [Label("视觉艺术")]
        VisualArts = 55,

        [Label("二手专区")]
        FleaMarket = 61,

        [Label("电子数码")]
        ElectronicsDigital = 66,

        [Label("程序员之家")]
        ProgrammerHome = 70,

        [Label("音乐空间")]
        Music = 74,

        [Label("店铺专区")]
        ShopZone = 111,

        [Label("文人墨客")]
        LiteratiInkGuest = 114,

        [Label("军事国防")]
        MilitaryDefense = 115,

        [Label("体坛风云")]
        SportsNews = 118,

        [Label("IC电设")]
        ICDesign = 121,

        [Label("动漫时代")]
        AnimationComicEra = 140,

        [Label("影视天地")]
        FilmAndTelevision = 149,

        [Label("就业创业")]
        EmploymentAndEntrepreneurship = 174,

        [Label("兼职信息发布栏")]
        PartTimeJobs = 183,

        [Label("保研考研")]
        GraduateRecommendationAndExamination = 199,

        [Label("社团交流中心")]
        ClubExchangeCenter = 208,

        [Label("出国留学")]
        StudyAbroad = 219,

        [Label("交通出行")]
        Transportation = 225,

        [Label("校园热点")]
        CampusHotTopics = 236,

        [Label("毕业感言")]
        GraduationReflection = 237,

        [Label("成电骑迹")]
        Cycling = 244,

        [Label("房屋租赁")]
        HouseLease = 255,

        [Label("失物招领")]
        LostAndFound = 305,

        [Label("成电锐评")]
        SharpCommentary = 309,

        [Label("跑步之家")]
        RunningHome = 312,

        [Label("鹊桥")]
        MagpieBridge = 313,

        [Label("自然科学")]
        NaturalScience = 316,

        [Label("新生专区")]
        Freshman = 326,

        [Label("情系舞缘")]
        DanceConnection = 334,

        [Label("吃喝玩乐")]
        Party = 370,

        [Label("考试专区")]
        Examination = 382,

        [Label("拼车同行")]
        Carpooling = 391,

        [Label("部门直通车")]
        DepartmentalExpress = 403,

        [Label("公考选调")]
        PublicExaminationSelectionAndTransfer = 430,

        [Label("投资理财")]
        InvestmentWealth = 888,
    }

    [JsonSerializable(typeof(Thread))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class ThreadContext : JsonSerializerContext { }
}
