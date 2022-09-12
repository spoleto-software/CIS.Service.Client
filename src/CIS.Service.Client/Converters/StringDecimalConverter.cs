using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CIS.Service.Client.Converters
{
    /// <summary>
    /// JSON converter with support decimal as string.
    /// </summary>
    public class StringDecimalConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    var str = reader.GetString();
                    var value = decimal.Parse(str, System.Globalization.CultureInfo.InvariantCulture);

                    return value;

                case JsonTokenType.Number:
                    return reader.GetDecimal();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) =>
            writer.WriteNumberValue(value);
    }
}
