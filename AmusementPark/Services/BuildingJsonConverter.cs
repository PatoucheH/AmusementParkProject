using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AmusementPark.Models; 

public class BuildingJsonConverter : JsonConverter<IBuilding>
{
    public override IBuilding Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (var jsonDoc = JsonDocument.ParseValue(ref reader))
        {
            var jsonObject = jsonDoc.RootElement;

            if (!jsonObject.TryGetProperty("Type", out var typeProperty))
                throw new JsonException("Missing Type discriminator");

            string typeDiscriminator = typeProperty.GetString();

            IBuilding building = typeDiscriminator switch
            {
                "RollerCoaster" => JsonSerializer.Deserialize<RollerCoaster>(jsonObject.GetRawText(), options),
                "HauntedHouse" => JsonSerializer.Deserialize<HauntedHouse>(jsonObject.GetRawText(), options),
                "GiftShop" => JsonSerializer.Deserialize<GiftShop>(jsonObject.GetRawText(), options),
                "FoodShop" => JsonSerializer.Deserialize<FoodShop>(jsonObject.GetRawText(), options),
                "DuckFishing" => JsonSerializer.Deserialize<DuckFishing>(jsonObject.GetRawText(), options),
                _ => throw new JsonException($"Unknown building type: {typeDiscriminator}")
            };

            return building;
        }
    }

    public override void Write(Utf8JsonWriter writer, IBuilding value, JsonSerializerOptions options)
    {
        var type = value.GetType().Name;

        var jsonObj = JsonSerializer.SerializeToElement(value, value.GetType(), options).Clone();

        using (writer)
        {
            writer.WriteStartObject();

            writer.WriteString("Type", type);

            foreach (var property in jsonObj.EnumerateObject())
            {
                property.WriteTo(writer);
            }

            writer.WriteEndObject();
        }
    }
}
