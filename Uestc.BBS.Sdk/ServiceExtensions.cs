using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Auth;
using Uestc.BBS.Sdk.Forum;
using Uestc.BBS.Sdk.Thread;

namespace Uestc.BBS.Sdk
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 注册 <see cref="IAuthService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseUrl">API 基地址</param>
        /// <returns></returns>
        public static IHttpClientBuilder AddAuthService(
            this IServiceCollection services,
            string baseUrl = ApiEndpoints.BASE_URL
        ) =>
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

        /// <summary>
        /// 注册 <see cref="IForumHomeService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseUrl">API 基地址</param>
        /// <returns></returns>
        public static IHttpClientBuilder AddForumHomeService(
            this IServiceCollection services,
            string baseUrl = ApiEndpoints.BASE_URL
        ) =>
            services.AddHttpClient<IForumHomeService, ForumHomeService>(
                (services, client) =>
                {
                    var authCredential = services.GetService<AuthCredential>();
                    if (authCredential != null)
                    {
                        // 不登陆可以获取论坛统计信息和公告，但不能获取新注册用户
                        client.DefaultRequestHeaders.Add("Cookie", authCredential.Cookie);
                        client.DefaultRequestHeaders.Add(
                            "Authorization",
                            authCredential.Authorization
                        );
                    }
                    client.BaseAddress = new Uri(baseUrl);
                }
            );

        /// <summary>
        /// 注册 <see cref="ISearchService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="baseUrl">API 基地址</param>
        /// <returns></returns>
        public static IHttpClientBuilder AddSearchService(
            this IServiceCollection services,
            string baseUrl = ApiEndpoints.BASE_URL
        ) =>
            services.AddHttpClient<ISearchService, SearchService>(
                (services, client) =>
                {
                    var authCredential = services.GetService<AuthCredential>();
                    if (authCredential != null)
                    {
                        client.DefaultRequestHeaders.Add("Cookie", authCredential.Cookie);
                        client.DefaultRequestHeaders.Add(
                            "Authorization",
                            authCredential.Authorization
                        );
                    }
                    client.BaseAddress = new Uri(baseUrl);
                }
            );
    }
}
