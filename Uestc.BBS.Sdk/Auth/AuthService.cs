using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.Auth
{
    public class AuthService(HttpClient httpClient) : IAuthService
    {
        public async Task LoginAsync(AuthCredential credential)
        {
            if (
                string.IsNullOrEmpty(credential.Username)
                || string.IsNullOrEmpty(credential.Password)
            )
            {
                throw new ArgumentException("Username or password is null or empty.");
            }

            await GetCookieAsync(credential.Username, credential.Password);
            await GetAuthorizationAsync().ContinueWith(t => credential.Autherization = t.Result);
            await GetMobcentTokenAsync(credential.Username, credential.Password)
                .ContinueWith(t =>
                {
                    credential.Uid = t.Result.Uid;
                    credential.Token = t.Result.Token;
                    credential.Secret = t.Result.Secret;
                });
        }

        /// <summary>
        /// 获取 Cookie
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<IEnumerable<string>> GetCookieAsync(string username, string password)
        {
            using var resp = await httpClient.PostAsync(
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
                )
            );

            return resp.StatusCode is HttpStatusCode.OK ? resp.Headers.GetValues("Set-Cookie") : [];
        }

        /// <summary>
        /// 获取 Authorization
        /// </summary>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        private async Task<string> GetAuthorizationAsync()
        {
            using var resp = await httpClient.PostAsync(ApiEndpoints.GET_AUTHORIZATION_URL, null);
            if (resp.StatusCode is not HttpStatusCode.OK)
            {
                throw new AuthenticationException(
                    $"Authorization failed, status code is {resp.StatusCode}."
                );
            }

            var authorization = JsonSerializer
                .Deserialize(
                    await resp.Content.ReadAsStreamAsync(),
                    AuthorizationRespContext.Default.AuthorizationResp
                )
                ?.Data.Token;

            return string.IsNullOrEmpty(authorization) is false
                ? authorization
                : throw new AuthenticationException(
                    "Authorization failed, token is null or empty."
                );
        }

        /// <summary>
        /// 获取 Mobcent Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        private async Task<MobcentAuthResp> GetMobcentTokenAsync(string username, string password)
        {
            using var resp = await httpClient.PostAsync(
                ApiEndpoints.GET_MOBCENT_TOKEN_URL,
                new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        { nameof(username), username },
                        { nameof(password), password },
                    }
                )
            );

            if (resp.StatusCode is not HttpStatusCode.OK)
            {
                throw new AuthenticationException(
                    "Mobcent token get failed, status code is not 200."
                );
            }

            return await JsonSerializer.DeserializeAsync(
                    await resp.Content.ReadAsStreamAsync(),
                    AuthRespContext.Default.MobcentAuthResp
                )
                ?? throw new AuthenticationException("Mobcent token get failed, response is null.");
        }
    }

    public class Authorization
    {
        [JsonPropertyName("authorization")]
        public string Token { get; set; } = string.Empty;
    }

    public class AuthorizationResp : ApiRespBase<Authorization> { }

    [JsonSerializable(typeof(AuthorizationResp))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    )]
    public partial class AuthorizationRespContext : JsonSerializerContext { }
}
