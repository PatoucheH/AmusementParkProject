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

        return typeName switch
        {
            nameof(GiftShop) => JsonSerializer.Deserialize<GiftShop>(root.GetRawText(), options),
            nameof(HauntedHouse) => JsonSerializer.Deserialize<HauntedHouse>(root.GetRawText(), options),
            nameof(RollerCoaster) => JsonSerializer.Deserialize<RollerCoaster>(root.GetRawText(), options),
            nameof(FoodShop) => JsonSerializer.Deserialize<FoodShop>(root.GetRawText(), options),
            nameof(DuckFishing) => JsonSerializer.Deserialize<DuckFishing>(root.GetRawText(), options),
            _ => throw new NotSupportedException($"Unsupported type: {typeName}")
        };

    }


    public override void Write(Utf8JsonWriter writer, IBuilding value, JsonSerializerOptions options)
    {
        var typeName = value.GetType().Name;

        var json = JsonSerializer.SerializeToElement(value, value.GetType(), options);
        using var doc = JsonDocument.Parse(json.GetRawText());

        writer.WriteStartObject();

        foreach (var prop in doc.RootElement.EnumerateObject())
            prop.WriteTo(writer);

        writer.WriteString("TypeName", typeName);

        writer.WriteEndObject();
    }

}
