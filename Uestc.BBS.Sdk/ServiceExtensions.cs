using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Uestc.BBS.Sdk.Auth;

namespace Uestc.BBS.Sdk
{
    public static class ServiceExtensions
    {
        public static IHttpClientBuilder AddAuthClient(this IServiceCollection services) =>
            services
                .AddHttpClient<IAuthService, AuthService>(client =>
                {
                    client.BaseAddress = new Uri(ApiEndpoints.BASE_URL);
                    client.DefaultRequestHeaders.Add("X-UESTC-BBS", "1");
                })
                .ConfigurePrimaryHttpMessageHandler(services => new HttpClientHandler
                {
                    UseCookies = true,
                    CookieContainer = services.GetRequiredService<AuthCredential>().Cookies,
                });
    }
}
