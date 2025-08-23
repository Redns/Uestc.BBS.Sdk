using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.JsonConverters
{
    public class ShortDateTimeConverter : JsonConverter<DateTime>
    {
        // 匹配 2025-8-16 22:05（月、日可能 1-2 位）
        private const string Format = "yyyy-M-d HH:mm";

        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var s = reader.GetString();
            return DateTime.ParseExact(s!, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
