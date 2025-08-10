namespace Uestc.BBS.Sdk.Auth
{
    /// <summary>
    /// 认证服务接口
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        Task LoginAsync(AuthCredential credential, CancellationToken cancellationToken = default);
    }
}
