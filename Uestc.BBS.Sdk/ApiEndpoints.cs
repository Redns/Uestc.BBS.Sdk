namespace Uestc.BBS.Sdk
{
    /// <summary>
    /// API 地址
    /// </summary>
    public static class ApiEndpoints
    {
        /// <summary>
        /// 基地址
        /// </summary>
        public const string BASE_URL = "https://bbs.uestc.edu.cn";

        public static readonly Uri BaseUri = new(BASE_URL);

        #region 登录相关
        /// <summary>
        /// 获取 Cookie
        /// </summary>
        public const string GET_COOKIE_URL =
            "member.php?mod=logging&action=login&loginsubmit=yes&inajax=1";

        /// <summary>
        /// 获取 Authorization
        /// </summary>
        public const string GET_AUTHORIZATION_URL = "star/api/v1/auth/adoptLegacyAuth";

        /// <summary>
        /// 获取 Mobcent token
        /// </summary>
        public const string GET_MOBCENT_TOKEN_URL = "mobcent/app/web/index.php?r=user/login";
        #endregion

        #region 论坛相关
        /// <summary>
        /// 获取首页信息
        /// </summary>
        public const string GET_GLOBAL_STATUS = "star/api/v1/index?global_stat=1";

        /// <summary>
        /// 获取公告列表
        /// </summary>
        public const string GET_ANNOUNCEMENT_LIST_URL = "star/api/v1/index?announcement=1";

        /// <summary>
        /// 获取每日一句
        /// </summary>
        public const string GET_DAILY_SENTENCE_URL = "forum.php?mobile=no";
        #endregion

        #region 主题相关
        /// <summary>
        /// 获取首页主题列表
        /// </summary>
        public const string GET_THREAD_TOPLIST_URL = "star/api/v1/forum/toplist";

        /// <summary>
        /// 获取主题列表
        /// </summary>
        public const string GET_THREAD_LIST_URL = "star/api/v1/thread/list";

        /// <summary>
        /// 发表主题评论
        /// </summary>
        public const string POST_THREAD_REPLY_URL = "star/api/v1/thread/reply";

        /// <summary>
        /// 获取首页主题列表（移动端）
        /// </summary>
        public const string GET_MOBILE_HOME_THREAD_LIST_URL = "mobcent/app/web/index.php";

        /// <summary>
        /// 获取主题内容（移动端）
        /// </summary>
        public const string GET_MOBILE_THREAD_CONTENT_URL = "mobcent/app/web/index.php";
        #endregion

        #region 消息相关
        /// <summary>
        /// 获取通知列表
        /// </summary>
        public const string GET_NOTIFICATIONS_URL = "star/api/v1/messages/notifications";
        #endregion

        #region 用户相关
        /// <summary>
        /// 用户头像
        /// </summary>
        public const string USER_AVATAR_URL = "uc_server/data/avatar/000";

        /// <summary>
        /// 用户好友
        /// </summary>
        public const string USER_FRIEND_LIST_URL = "home.php";
        #endregion
    }
}
