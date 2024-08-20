using System.Text.Json;
using System.Text.Json.Serialization;

namespace Carefusion.Core
{
    public abstract class Utilities
    {
        public class DateOnlyJsonConverter : JsonConverter<DateOnly>
        {
            private const string Format = "yyyy-MM-dd";

            public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateOnly.ParseExact(reader.GetString() ?? string.Empty, Format);
            }

            public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(Format));
            }
        }

        public class NotFoundException(string message) : Exception(message)
        {
        }

    }
}
