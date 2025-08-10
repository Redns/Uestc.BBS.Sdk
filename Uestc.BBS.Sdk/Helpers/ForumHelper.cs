namespace Uestc.BBS.Sdk.Helpers
{
    public static class ForumHelper
    {
        /// <summary>
        /// 获取用户头像地址
        /// </summary>
        /// <param name="uid">用户 ID</param>
        /// <param name="isLarge">是否获取大头像</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetUserAvaterUrl(this uint uid, bool isLarge = false)
        {
            var uidString = uid.ToString();
            if (uidString.Length != 6)
            {
                throw new ArgumentException("Uid length should be 6");
            }

            return string.Format(
                "{0}/{1}/{2}/{3}",
                ApiEndpoints.BASE_URL,
                ApiEndpoints.USER_AVATAR_URL,
                $"{uidString[..2]}/{uidString[2..4]}/{uidString[4..]}",
                isLarge ? "_avatar_big.jpg" : "_avatar_middle.jpg"
            );
        }
    }
}
