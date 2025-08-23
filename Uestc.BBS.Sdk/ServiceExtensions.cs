using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Services.Auth;
using Uestc.BBS.Sdk.Services.Forum;
using Uestc.BBS.Sdk.Services.System;
using Uestc.BBS.Sdk.Services.Thread;
using Uestc.BBS.Sdk.Services.Thread.ThreadContent;
using Uestc.BBS.Sdk.Services.Thread.ThreadList;
using Uestc.BBS.Sdk.Services.Thread.ThreadSearch;

namespace Uestc.BBS.Sdk
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 注册 <see cref="IAuthService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddAuthService(
            this IServiceCollection services,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services.AddHttpClient<IAuthService, AuthService>(
                (services, client) =>
                {
                    client.BaseAddress = getBaseUrl is not null
                        ? getBaseUrl(services)
                        : ApiEndpoints.BaseUri;
                }
            );

        /// <summary>
        /// 注册 <see cref="IThreadListService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddMobcentThreadListService(
            this IServiceCollection services,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services.AddHttpClient<IThreadListService, MobcentThreadListService>(
                (services, client) =>
                {
                    client.BaseAddress = getBaseUrl is not null
                        ? getBaseUrl(services)
                        : ApiEndpoints.BaseUri;
                }
            );

        /// <summary>
        /// 注册 <see cref="IThreadListService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddWebThreadListService(
            this IServiceCollection services,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services.AddHttpClient<IThreadListService, WebThreadListService>(
                (services, client) =>
                {
                    var authCredential = services.GetService<AuthCredential>();
                    if (authCredential != null)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(
                            "Cookie",
                            authCredential.Cookie
                        );
                        client.DefaultRequestHeaders.TryAddWithoutValidation(
                            "Authorization",
                            authCredential.Authorization
                        );
                    }
                    client.BaseAddress = getBaseUrl is not null
                        ? getBaseUrl(services)
                        : ApiEndpoints.BaseUri;
                }
            );

        /// <summary>
        /// 注册 <see cref="IThreadContentService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddMobcentThreadContentService(
            this IServiceCollection services,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services.AddHttpClient<IThreadContentService, MobcentThreadContentService>(
                (services, client) =>
                {
                    client.BaseAddress = getBaseUrl is not null
                        ? getBaseUrl(services)
                        : ApiEndpoints.BaseUri;
                }
            );

        /// <summary>
        /// 注册 <see cref="IForumHomeService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl">API 基地址</param>
        /// <returns></returns>
        public static IHttpClientBuilder AddForumHomeService(
            this IServiceCollection services,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services.AddHttpClient<IForumHomeService, ForumHomeService>(
                (services, client) =>
                {
                    var authCredential = services.GetService<AuthCredential>();
                    if (authCredential != null)
                    {
                        // 不登陆可以获取论坛统计信息和公告，但不能获取新注册用户
                        client.DefaultRequestHeaders.TryAddWithoutValidation(
                            "Cookie",
                            authCredential.Cookie
                        );
                        client.DefaultRequestHeaders.TryAddWithoutValidation(
                            "Authorization",
                            authCredential.Authorization
                        );
                    }
                    client.BaseAddress = getBaseUrl is not null
                        ? getBaseUrl(services)
                        : ApiEndpoints.BaseUri;
                }
            );

        /// <summary>
        /// 注册 <see cref="ISearchService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl">API 基地址</param>
        /// <returns></returns>
        public static IHttpClientBuilder AddSearchService(
            this IServiceCollection services,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services.AddHttpClient<ISearchService, SearchService>(
                (services, client) =>
                {
                    var authCredential = services.GetService<AuthCredential>();
                    if (authCredential != null)
                    {
                        client.DefaultRequestHeaders.TryAddWithoutValidation(
                            "Cookie",
                            authCredential.Cookie
                        );
                        client.DefaultRequestHeaders.TryAddWithoutValidation(
                            "Authorization",
                            authCredential.Authorization
                        );
                    }
                    client.BaseAddress = getBaseUrl is not null
                        ? getBaseUrl(services)
                        : ApiEndpoints.BaseUri;
                }
            );

        public static IHttpClientBuilder AddDailySentencesService(
            this IServiceCollection services,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services.AddHttpClient<IDailySentenceService, DailySentenceService>(
                (services, client) =>
                {
                    client.BaseAddress = getBaseUrl is not null
                        ? getBaseUrl(services)
                        : ApiEndpoints.BaseUri;
                }
            );
    }
}
