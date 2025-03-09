using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.JsonConverters
{
    public class CookieContainerConverter : JsonConverter<CookieContainer>
    {
        public override CookieContainer Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var cookieContainer = new CookieContainer();
            var cookeCollection =
                JsonSerializer.Deserialize(
                    ref reader,
                    CookieCollectionContext.Default.CookieCollection
                ) ?? throw new JsonException("Invalid cookie collection");
            cookieContainer.Add(cookeCollection);

            return cookieContainer;
        }

        public override void Write(
            Utf8JsonWriter writer,
            CookieContainer value,
            JsonSerializerOptions options
        ) =>
            JsonSerializer.Serialize(
                writer,
                value.GetAllCookies(),
                CookieCollectionContext.Default.CookieCollection
            );
    }

    [JsonSerializable(typeof(CookieCollection))]
    [JsonSourceGenerationOptions(
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
    )]
    public partial class CookieCollectionContext : JsonSerializerContext { }
}
