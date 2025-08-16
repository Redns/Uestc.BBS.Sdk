using System.Security.Authentication;
using System.Text.Json;

namespace Uestc.BBS.Sdk.Services.Auth
{
    /// <summary>
    /// 认证服务
    /// </summary>
    /// <param name="client">请使用 <see cref="ServiceExtensions.CreateAuthClient"/>  注入</param>
    public class AuthService(HttpClient client) : IAuthService
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

            // 获取 Cookie 和 Authorization
            var (cookie, authorization) = await GetCookieAndAuthorizationAsync(
                credential.Username,
                credential.Password,
                cancellationToken
            );
            credential.Cookie = cookie;
            credential.Authorization = authorization;

            // 获取 Token 和 Secret
            var mobcentAuthorizationResult = await GetMobcentTokenAsync(
                credential.Username,
                credential.Password,
                cancellationToken
            );

            credential.Uid = mobcentAuthorizationResult.Uid;
            credential.Token = mobcentAuthorizationResult.Token;
            credential.Secret = mobcentAuthorizationResult.Secret;
            credential.Avatar = mobcentAuthorizationResult.Avatar;
            credential.Level = mobcentAuthorizationResult.UserTitleLevel;
        }

        /// <summary>
        /// 获取 Cookie 和 Authorization
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        private async Task<(string cookie, string authorization)> GetCookieAndAuthorizationAsync(
            string username,
            string password,
            CancellationToken cancellationToken = default
        )
        {
            // 获取 Cookie
            using var cookieResp = await client.PostAsync(
                ApiEndpoints.GET_COOKIE_URL,
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { nameof(username), username },
                        { nameof(password), password },
                        { "fastloginfield", "username" },
                        { "cookietime", "2592000" },
                        { "questionid", "0" },
                        { "answer", string.Empty },
                    }
                ),
                cancellationToken
            );
            cookieResp.EnsureSuccessStatusCode();

            var cookie = string.Join(
                ";",
                cookieResp.Headers.GetValues("Set-Cookie").Select(c => c.Split(';').First())
            );
            client.DefaultRequestHeaders.Add("Cookie", cookie);
            client.DefaultRequestHeaders.Add("X-UESTC-BBS", "1");

            // 获取 Authorization
            using var authResp = await client.PostAsync(
                ApiEndpoints.GET_AUTHORIZATION_URL,
                null,
                cancellationToken
            );
            authResp.EnsureSuccessStatusCode();

            using var authRespStream = await authResp.Content.ReadAsStreamAsync(cancellationToken);
            var authorizationResult = await JsonSerializer.DeserializeAsync(
                authRespStream,
                AuthorizationRespContext.Default.AuthorizationResp,
                cancellationToken
            );
            var authorization =
                authorizationResult?.Data?.Token
                ?? throw new AuthenticationException(
                    "Authorization failed, token is null or empty."
                );

            cookie = string.Concat(
                cookie,
                ';',
                string.Join(
                    ";",
                    authResp.Headers.GetValues("Set-Cookie").Select(c => c.Split(';').First())
                )
            );

            return (cookie, authorization);
        }

        /// <summary>
        /// 获取 Mobcent Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        private async Task<MobcentAuthResp> GetMobcentTokenAsync(
            string username,
            string password,
            CancellationToken cancellationToken = default
        )
        {
            using var resp = await client.PostAsync(
                ApiEndpoints.GET_MOBCENT_TOKEN_URL,
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { nameof(username), username },
                        { nameof(password), password },
                    }
                ),
                cancellationToken
            );
            resp.EnsureSuccessStatusCode();

            using var respStream = await resp.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync(
                    respStream,
                    AuthRespContext.Default.MobcentAuthResp,
                    cancellationToken
                )
                ?? throw new AuthenticationException("Mobcent token get failed, response is null.");
        }
    }
}
