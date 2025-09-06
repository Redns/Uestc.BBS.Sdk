using System.Text.RegularExpressions;

namespace Uestc.BBS.Sdk.Services.System
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="httpClientFactory">请使用 <see cref="ServiceExtensions.UseWebServices"/> 注入</param>
    public partial class DailySentenceService(IHttpClientFactory httpClientFactory)
        : IDailySentenceService
    {
        /// <summary>
        /// 获取每日一句
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDailySentenceAsync(
            CancellationToken cancellationToken = default
        )
        {
            var httpClient = httpClientFactory.CreateClient(ServiceExtensions.WEB_API);
            var content = await httpClient.GetStringAsync(
                ApiEndpoints.GET_DAILY_SENTENCE_URL,
                cancellationToken
            );

            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }

            var match = DailySentenceRegex().Match(content);
            if (match.Success)
            {
                return match.Groups[1].Value.Trim();
            }

            return string.Empty;
        }

        [GeneratedRegex(
            @"<div class=""vanfon_geyan"">.*?<span[^>]*>(.*?)<\/span>.*?<\/div>",
            RegexOptions.Singleline
        )]
        private static partial Regex DailySentenceRegex();
    }
}
