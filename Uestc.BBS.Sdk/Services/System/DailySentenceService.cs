﻿using System.Text.RegularExpressions;

namespace Uestc.BBS.Sdk.Services.System
{
    public partial class DailySentenceService(HttpClient httpClient) : IDailySentenceService
    {
        private readonly HttpClient _httpClient = httpClient;

        /// <summary>
        /// 获取每日一句
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDailySentenceAsync(
            CancellationToken cancellationToken = default
        )
        {
            var content = await _httpClient.GetStringAsync(
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
