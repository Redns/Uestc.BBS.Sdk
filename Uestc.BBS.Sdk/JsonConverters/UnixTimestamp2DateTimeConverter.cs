using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.JsonConverters
{
    /// <summary>
    /// Unix 秒级时间戳转换为 <see cref="DateTime"/> 类型
    /// </summary>
    public class UnixTimestamp2DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        ) =>
            reader.TokenType is JsonTokenType.Number
                ? DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).LocalDateTime
                : throw new JsonException("Expected an Int value for DateTime.");

        public override void Write(
            Utf8JsonWriter writer,
            DateTime value,
            JsonSerializerOptions options
        )
        {
            var unixTime = new DateTimeOffset(value.ToUniversalTime()).ToUnixTimeSeconds();
            writer.WriteNumberValue(unixTime);
        }
    }
}
