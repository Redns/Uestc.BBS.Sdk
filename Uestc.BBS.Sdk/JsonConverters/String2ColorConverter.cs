using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Uestc.BBS.Sdk.JsonConverters
{
    /// <summary>
    /// 字符串转 <see cref="Color"/>
    /// </summary>
    public class String2ColorConverter : JsonConverter<Color>
    {
        public override Color Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var hex = reader.GetString();

            return string.IsNullOrEmpty(hex) || !hex.StartsWith('#')
                ? Color.Empty
                : Color.FromArgb(Convert.ToInt32(hex[1..], 16));
        }

        public override void Write(
            Utf8JsonWriter writer,
            Color value,
            JsonSerializerOptions options
        ) => writer.WriteStringValue($"#{value.R:X2}{value.G:X2}{value.B:X2}");
    }
}
