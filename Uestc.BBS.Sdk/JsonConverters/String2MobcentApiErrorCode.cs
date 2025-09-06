using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.JsonConverters
{
    public class String2MobcentApiErrorCode : JsonConverter<MobcentApiErrorCode>
    {
        public override MobcentApiErrorCode Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var errorCode = reader.GetString();
            return errorCode switch
            {
                "00000000" => MobcentApiErrorCode.Success,
                "00200102" => MobcentApiErrorCode.Unauthenticated,
                _ => MobcentApiErrorCode.Unkown,
            };
        }

        public override void Write(
            Utf8JsonWriter writer,
            MobcentApiErrorCode value,
            JsonSerializerOptions options
        )
        {
            throw new NotImplementedException();
        }
    }
}
