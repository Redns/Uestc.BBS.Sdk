using System.Text.Json;
using System.Text.Json.Serialization;
using FastEnumUtility;
using Uestc.BBS.Sdk.Services.Message;

namespace Uestc.BBS.Sdk.JsonConverters
{
    public class String2MessageTypeConverter : JsonConverter<MessageType>
    {
        public override MessageType Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType is not JsonTokenType.String)
            {
                throw new ArgumentException("Invalid message type");
            }

            var messageType = reader.GetString();
            if (messageType is "post_other")
            {
                return MessageType.Other;
            }

            if (FastEnum.TryParse(messageType, out MessageType ret))
            {
                return ret;
            }

            throw new ArgumentException("Unknown message type " + messageType);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MessageType value,
            JsonSerializerOptions options
        )
        {
            throw new NotImplementedException();
        }
    }
}
