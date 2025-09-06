using System.Security.Authentication;
using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Auth
{
    /// <summary>
    /// 认证服务
    /// </summary>
    /// <param name="httpClientFactory">请使用 <see cref="ServiceExtensions.UseWebServices"/> 注入</param>
    public class WebAuthService(IHttpClientFactory httpClientFactory) : IAuthService
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
                throw new ArgumentException("username/password is null or empty.");
            }

            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);

            // 获取 Cookie
            using var cookieResp = await httpClient.PostAsync(
                ApiEndpoints.GET_COOKIE_URL,
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { "username", credential.Username },
                        { "password", credential.Password },
                        { "fastloginfield", "username" },
                        { "cookietime", "2592000" },
                        { "questionid", "0" },
                        { "answer", string.Empty },
                    }
                ),
                cancellationToken
            );
            cookieResp.EnsureSuccessStatusCode();

            // 获取 Authorization
            httpClient.DefaultRequestHeaders.Add("X-UESTC-BBS", "1");
            using var authResp = await httpClient.PostAsync(
                ApiEndpoints.GET_AUTHORIZATION_URL,
                null,
                cancellationToken
            );
            authResp.EnsureSuccessStatusCode();

            using var authRespStream = await authResp.Content.ReadAsStreamAsync(cancellationToken);
            var authResult = await JsonSerializer.DeserializeAsync(
                authRespStream,
                AuthorizationRespContext.Default.AuthorizationResp,
                cancellationToken
            );
            credential.Authorization =
                authResult?.Data?.Token
                ?? throw new AuthenticationException(
                    "Authorization failed, token is null or empty."
                );

            // 补充 Cookie
            foreach (var cookie in authResp.Headers.GetValues("Set-Cookie"))
            {
                credential.CookieContainer.SetCookies(
                    cookieResp.RequestMessage!.RequestUri!,
                    cookie
                );
            }

            // 获取用户信息
            if (authResult.User is null)
            {
                throw new AuthenticationException("Authorization failed, user is null.");
            }

            credential.Uid = authResult.User.Uid;
            // TODO 补充用户信息
            // credential.Avatar = mobcentAuthorizationResult.Avatar;
            // credential.Level = mobcentAuthorizationResult.UserTitleLevel;
        }
    }
}
