using AmusementPark.Data;
using AmusementPark.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

public class BuildingJsonConverter : JsonConverter<IBuilding>
{
    public override IBuilding? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var typeName = root.GetProperty("TypeName").GetString();

        var localOptions = new JsonSerializerOptions(options);
        localOptions.Converters.Add(new PositionJsonConverter());

        return typeName switch
        {
            nameof(GiftShop) => JsonSerializer.Deserialize<GiftShop>(root.GetRawText(), localOptions),
            nameof(HauntedHouse) => JsonSerializer.Deserialize<HauntedHouse>(root.GetRawText(), localOptions),
            nameof(RollerCoaster) => JsonSerializer.Deserialize<RollerCoaster>(root.GetRawText(), localOptions),
            nameof(FoodShop) => JsonSerializer.Deserialize<FoodShop>(root.GetRawText(), localOptions),
            nameof(DuckFishing) => JsonSerializer.Deserialize<DuckFishing>(root.GetRawText(), localOptions),
            _ => throw new NotSupportedException($"Unsupported type: {typeName}")
        };

    }


    public override void Write(Utf8JsonWriter writer, IBuilding value, JsonSerializerOptions options)
    {
        var typeName = value.GetType().Name;

        var localOptions = new JsonSerializerOptions(options);
        localOptions.Converters.Add(new PositionJsonConverter());

        var json = JsonSerializer.SerializeToElement(value, value.GetType(), localOptions);
        using var doc = JsonDocument.Parse(json.GetRawText());

        writer.WriteStartObject();

        foreach (var prop in doc.RootElement.EnumerateObject())
            prop.WriteTo(writer);

        writer.WriteString("TypeName", typeName);

        writer.WriteEndObject();
    }

}
