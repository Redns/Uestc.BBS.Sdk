using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.JsonConverters
{
    public class IntToBoolConverter : JsonConverter<bool>
    {
        public override bool Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType is not JsonTokenType.Number)
            {
                throw new JsonException("Expected a number value for boolean.");
            }
            return reader.GetInt32() != 0;
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value ? 1 : 0);
        }
    }
}
