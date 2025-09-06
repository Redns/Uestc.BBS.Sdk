using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.Services.Thread.ThreadList;
using Uestc.BBS.Sdk.Services.User;

namespace Uestc.BBS.Sdk.Services.Thread
{
    /// <summary>
    /// 主题（帖子）
    /// </summary>
    public class ThreadOverview
    {
        /// <summary>
        /// 主题 ID
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// 板块
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// 板块名称
        /// </summary>
        public string BoardName { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 摘要
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 热门帖子为发表时间，其余帖子为最新回复时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 是否为热门主题
        /// </summary>
        public bool IsHot { get; set; }

        /// <summary>
        /// 预览图像
        /// </summary>
        public string[] PreviewImageSources { get; set; } = [];

        /// <summary>
        /// 浏览量
        /// </summary>
        public uint ViewCount { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        public uint ReplyCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public uint LikeCount { get; set; }

        /// <summary>
        /// 点踩数量
        /// </summary>
        public uint DislikeCount { get; set; }

        /// <summary>
        /// 用户 ID
        /// </summary>
        public uint Uid { get; set; }

        /// <summary>
        /// 是否匿名
        /// </summary>
        public bool IsAnonymous => Uid == 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; } = string.Empty;

        /// <summary>
        /// 用户性别
        /// </summary>
        public Gender UserGender { get; set; }
    }

    /// <summary>
    /// 版块 ID 枚举
    /// </summary>
    public enum Board
    {
        /// <summary>
        /// XXX 热门板块实际上不需要 Board 参数，此处是用于标记热门版块主题
        /// </summary>
        [Label("热门", (int)TopicSortType.All)]
        Hot = -1,

        [Label("最新发表", (int)TopicSortType.New)]
        [Label("最新回复", (int)TopicSortType.All)]
        [Label("精华", (int)TopicSortType.Essence)]
        Latest = 0,

        [Label("站务公告")]
        StationAnnouncement = 2,

        [Label("同城同乡")]
        CityTownship = 17,

        [Label("学术交流")]
        AcademicExchange = 20,

        [Label("水手之家")]
        WaterHome = 25,

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

    /// <summary>
    /// 帖子类型
    /// </summary>
    public enum ThreadType
    {
        [Label("普通")]
        Normal = 0,

        [Label("置顶公告")]
        NormalComplex,

        [Label("投票")]
        Vote,
    }

    /// <summary>
    /// 主题渲染格式
    /// </summary>
    public enum ThreadRenderFormat
    {
        Unknown = -1, // TODO 未知
        Defalut = 0, // TODO 默认
        Unknown2, // TODO 未知
        Markdown, // Markdown
    }

    [JsonSerializable(typeof(ThreadOverview))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class ThreadContext : JsonSerializerContext { }
}
