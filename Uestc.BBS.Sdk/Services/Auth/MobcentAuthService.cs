using System.Security.Authentication;
using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Auth
{
    /// <summary>
    /// 认证服务
    /// </summary>
    /// <param name="httpClientFactory">请使用 <see cref="ServiceExtensions.UseMobcentServices"/> 注入</param>
    public class MobcentAuthService(IHttpClientFactory httpClientFactory) : IAuthService
    {
        /// <summary>
        /// 登录获取认证信息
        /// </summary>
        /// <param name="credential">登录凭证</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">输入用户名或密码为空</exception>
        public async Task LoginAsync(AuthCredential credential, CancellationToken cancellationToken)
        {
            if (
                string.IsNullOrEmpty(credential.Username)
                || string.IsNullOrEmpty(credential.Password)
            )
            {
                throw new ArgumentException("Username/Password is null or empty.");
            }

            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.MOBCENT_API);

            // 获取 Token 和 Secret
            using var resp = await httpClient.PostAsync(
                ApiEndpoints.GET_MOBCENT_TOKEN_URL,
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "username", credential.Username },
                        { "password", credential.Password },
                    }
                ),
                cancellationToken
            );
            resp.EnsureSuccessStatusCode();

            using var respStream = await resp.Content.ReadAsStreamAsync(cancellationToken);
            var mobcentAuthorizationResult =
                await JsonSerializer.DeserializeAsync(
                    respStream,
                    AuthRespContext.Default.MobcentAuthResp,
                    cancellationToken
                )
                ?? throw new AuthenticationException("Mobcent token get failed, response is null.");

            credential.Uid = mobcentAuthorizationResult.Uid;
            credential.Token = mobcentAuthorizationResult.Token;
            credential.Secret = mobcentAuthorizationResult.Secret;
            credential.Avatar = mobcentAuthorizationResult.Avatar;
            credential.Level = mobcentAuthorizationResult.UserTitleLevel;
        }
    }
}
