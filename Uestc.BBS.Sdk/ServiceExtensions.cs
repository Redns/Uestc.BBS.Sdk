using System.Net;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Services.Auth;
using Uestc.BBS.Sdk.Services.Forum;
using Uestc.BBS.Sdk.Services.System;
using Uestc.BBS.Sdk.Services.Thread;
using Uestc.BBS.Sdk.Services.Thread.ThreadContent;
using Uestc.BBS.Sdk.Services.Thread.ThreadList;
using Uestc.BBS.Sdk.Services.Thread.ThreadReply;
using Uestc.BBS.Sdk.Services.Thread.ThreadSearch;

namespace Uestc.BBS.Sdk
{
    public static class ServiceExtensions
    {
        public const string WEB_API = "web";

        public const string MOBCENT_API = "mobcent";

        public static IHttpClientBuilder UseWebServices(
            this IServiceCollection services,
            Func<IServiceProvider, CookieContainer?> getCookies,
            Func<IServiceProvider, string> getAuthorization,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services
                .AddHttpClient(
                    WEB_API,
                    (services, client) =>
                    {
                        client.BaseAddress = getBaseUrl is not null
                            ? getBaseUrl(services)
                            : ApiEndpoints.BaseUri;
                    }
                )
                .ConfigureHttpClient(
                    (services, client) =>
                    {
                        var authorization = getAuthorization(services);
                        if (string.IsNullOrEmpty(authorization))
                        {
                            return;
                        }

                        // Authorization
                        client.DefaultRequestHeaders.TryAddWithoutValidation(
                            "Authorization",
                            authorization
                        );
                    }
                )
                .ConfigurePrimaryHttpMessageHandler(
                    (handler, services) =>
                    {
                        if (handler is not SocketsHttpHandler socketsHttpHandler)
                        {
                            return;
                        }

                        var cookieContainer = getCookies(services);
                        if (cookieContainer is null)
                        {
                            return;
                        }

                        // Cookie
                        socketsHttpHandler.UseCookies = true;
                        socketsHttpHandler.CookieContainer = getCookies(services);
                    }
                )
                .AddAsKeyed();

        public static IHttpClientBuilder UseMobcentServices(
            this IServiceCollection services,
            Func<IServiceProvider, (string token, string secret)> getTokenAndSecret,
            Func<IServiceProvider, Uri>? getBaseUrl = null
        ) =>
            services
                .AddHttpClient(
                    MOBCENT_API,
                    (services, client) =>
                    {
                        client.BaseAddress = getBaseUrl is not null
                            ? getBaseUrl(services)
                            : ApiEndpoints.BaseUri;
                    }
                )
                .AddHttpMessageHandler(services =>
                {
                    var (token, secret) = getTokenAndSecret(services);
                    return new MobcentAuthorizationHandler(token, secret);
                })
                .AddAsKeyed();

        /// <summary>
        /// 注册 <see cref="IAuthService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebAuthService(this IServiceCollection services) =>
            services.AddKeyedTransient<IAuthService, WebAuthService>(WEB_API);

        /// <summary>
        /// 注册 <see cref="IAuthService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IServiceCollection AddMobcentAuthService(this IServiceCollection services) =>
            services.AddKeyedTransient<IAuthService, MobcentAuthService>(MOBCENT_API);

        /// <summary>
        /// 注册 <see cref="IThreadListService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IServiceCollection AddMobcentThreadListService(
            this IServiceCollection services
        ) => services.AddKeyedTransient<IThreadListService, MobcentThreadListService>(MOBCENT_API);

        /// <summary>
        /// 注册 <see cref="IThreadListService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebThreadListService(
            this IServiceCollection services
        ) => services.AddKeyedTransient<IThreadListService, WebThreadListService>(WEB_API);

        /// <summary>
        /// 注册 <see cref="IThreadContentService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IServiceCollection AddMobcentThreadContentService(
            this IServiceCollection services
        ) =>
            services.AddKeyedTransient<IThreadContentService, MobcentThreadContentService>(
                MOBCENT_API
            );

        /// <summary>
        /// 注册 <see cref="IThreadReplyService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebThreadReplyService(
            this IServiceCollection services
        ) => services.AddKeyedTransient<IThreadReplyService, WebThreadReplyService>(WEB_API);

        /// <summary>
        /// 注册 <see cref="IForumHomeService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl">API 基地址</param>
        /// <returns></returns>
        public static IServiceCollection AddForumHomeService(this IServiceCollection services) =>
            services.AddTransient<IForumHomeService, ForumHomeService>();

        /// <summary>
        /// 注册 <see cref="ISearchService"/> 实例
        /// </summary>
        /// <param name="services"></param>
        /// <param name="getBaseUrl">API 基地址</param>
        /// <returns></returns>
        public static IServiceCollection AddSearchService(this IServiceCollection services) =>
            services.AddTransient<ISearchService, SearchService>();

        public static IServiceCollection AddDailySentencesService(
            this IServiceCollection services
        ) => services.AddTransient<IDailySentenceService, DailySentenceService>();
    }

    public class MobcentAuthorizationHandler(string token, string secret) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            if (request.RequestUri is not null)
            {
                var ub = new UriBuilder(request.RequestUri);
                var q = HttpUtility.ParseQueryString(ub.Query ?? string.Empty);
                q["accessToken"] = token;
                q["accessSecret"] = secret;
                ub.Query = q.ToString();
                request.RequestUri = ub.Uri;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
