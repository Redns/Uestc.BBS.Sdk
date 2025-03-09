namespace Uestc.BBS.Sdk
{
    public static class ApiEndpoints
    {
        /// <summary>
        /// 基地址
        /// </summary>
        public const string BASE_URL = "https://bbs.uestc.edu.cn";

        /// <summary>
        /// 获取 Cookie
        /// </summary>
        public const string GET_COOKIE_URL =
            "member.php?mod=logging&action=login&loginsubmit=yes&inajax=1";

        /// <summary>
        /// 获取 Authorization
        /// </summary>
        public const string GET_AUTHORIZATION_URL = "star/api/v1/auth/adoptLegacyAuth";

        public const string GET_MOBCENT_TOKEN_URL = "mobcent/app/web/index.php?r=user/login";
    }
}
