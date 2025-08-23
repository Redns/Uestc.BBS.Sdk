using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.JsonConverters
{
    public class AnythingToUintConverter : JsonConverter<uint>
    {
        public override uint Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType is JsonTokenType.Number)
            {
                return reader.GetUInt32();
            }

            if (reader.TokenType is JsonTokenType.String)
            {
                if (uint.TryParse(reader.GetString(), out uint ret))
                {
                    return ret;
                }
                return 0;
            }

            return 0;
        }

        public override void Write(Utf8JsonWriter writer, uint value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
