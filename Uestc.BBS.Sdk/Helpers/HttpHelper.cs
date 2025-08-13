namespace Uestc.BBS.Sdk.Helpers
{
    public static class HttpHelper
    {
        public static string ToQueryString(this IEnumerable<KeyValuePair<string, string?>> pairs) =>
            string.Join(
                '&',
                pairs.Select(p =>
                    $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value ?? string.Empty)}"
                )
            );
    }
}
