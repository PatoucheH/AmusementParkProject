using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using AmusementPark.Models; 
    using System.Text.Json;
    using System.Text.Json.Serialization;

namespace AmusementPark.Data
{
    public class PositionJsonConverter : JsonConverter<Position>
    {
        public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var json = JsonDocument.ParseValue(ref reader).RootElement;
            int x = json.GetProperty("X").GetInt32();
            int y = json.GetProperty("Y").GetInt32();
            return new Position(x, y);
        }

        public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteEndObject();
        }
    }

}
