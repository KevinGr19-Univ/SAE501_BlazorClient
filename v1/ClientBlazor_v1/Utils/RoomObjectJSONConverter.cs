using ClientBlazor_v1.Models.RoomObjects;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ClientBlazor_v1.Utils
{
    public class RoomObjectJSONConverter : JsonConverter<RoomObject>
    {
        private class RoomObjectDiscriminator
        {
            public string RoomObjectType { get; set; }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableTo(typeof(RoomObject));// && !typeToConvert.IsAssignableTo(typeof(Sensor));
        }

        public override RoomObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            options = new JsonSerializerOptions(options);
            options.Converters.Remove(this);

            if (typeToConvert == typeof(RoomObject))
            {
                var reader_clone = reader;
                var discriminator = JsonSerializer.Deserialize<RoomObjectDiscriminator?>(ref reader_clone, options);
                if (discriminator is null) return null;

                var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == discriminator.RoomObjectType);
                if (type is null) return null;

                typeToConvert = type;
            }

            return (RoomObject?)JsonSerializer.Deserialize(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, RoomObject value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            writer.WritePropertyName(nameof(RoomObjectDiscriminator.RoomObjectType));
            writer.WriteStringValue(value.GetType().Name);
            foreach (var property in value.GetType().GetProperties())
            {
                if (!property.CanRead || property.GetCustomAttribute<JsonIgnoreAttribute>() is not null)
                    continue;

                writer.WritePropertyName(property.Name);
                JsonSerializer.Serialize(writer, property.GetValue(value), options);
            }
            writer.WriteEndObject();
        }
    }
}
