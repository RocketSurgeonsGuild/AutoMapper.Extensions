using System.Text.Json;
using AutoMapper;

namespace Rocket.Surgery.Extensions.AutoMapper.SystemTextJson;

public class JsonElementConverter :
    ITypeConverter<JsonElement, byte[]?>,
    ITypeConverter<byte[]?, JsonElement>,
    ITypeConverter<JsonElement, string?>,
    ITypeConverter<string?, JsonElement>,
    ITypeConverter<JsonElement?, byte[]?>,
    ITypeConverter<byte[]?, JsonElement?>,
    ITypeConverter<JsonElement?, string?>,
    ITypeConverter<string?, JsonElement?>,
    ITypeConverter<JsonElement, JsonElement>,
    ITypeConverter<JsonElement?, JsonElement>,
    ITypeConverter<JsonElement, JsonElement?>,
    ITypeConverter<JsonElement?, JsonElement?>
{
    private static readonly JsonElement _empty = JsonSerializer.Deserialize<JsonElement>("null");

    private static JsonDefaultValue GetJsonDefaultValue(ResolutionContext context)
    {
        return JsonDefaultValue.Default;
    }

    private static JsonElement GetDefault(JsonElement value, ResolutionContext context) => GetJsonDefaultValue(context) switch
    {
        JsonDefaultValue.NotNull => value.ValueKind == JsonValueKind.Undefined ? _empty : value,
        _                        => value
    };

    private static JsonElement? GetDefault(JsonElement? value, ResolutionContext context) => GetJsonDefaultValue(context) switch
    {
        JsonDefaultValue.NotNull => !value.HasValue || value.Value.ValueKind == JsonValueKind.Undefined
            ? _empty
            : value.Value,
        _ => value
    };

    public byte[]? Convert(JsonElement source, byte[]? destination, ResolutionContext context)
    {
        return source.ValueKind == JsonValueKind.Undefined
            ? destination
            : JsonSerializer.SerializeToUtf8Bytes(source);
    }

    public JsonElement Convert(byte[]? source, JsonElement destination, ResolutionContext context)
    {
        return source == null || source.Length == 0
            ? GetDefault(destination, context)
            : JsonSerializer.Deserialize<JsonElement>(source);
    }

    public string? Convert(JsonElement source, string? destination, ResolutionContext context)
    {
        return source.ValueKind == JsonValueKind.Undefined
            ? destination
            : JsonSerializer.Serialize(source);
    }

    public JsonElement Convert(string? source, JsonElement destination, ResolutionContext context)
    {
        return string.IsNullOrEmpty(source)
            ? GetDefault(destination, context)
            : JsonSerializer.Deserialize<JsonElement>(source);
    }

    public byte[]? Convert(JsonElement? source, byte[]? destination, ResolutionContext context)
    {
        return !source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined
            ? destination
            : JsonSerializer.SerializeToUtf8Bytes(source);
    }

    public JsonElement? Convert(byte[]? source, JsonElement? destination, ResolutionContext context)
    {
        return source == null || source.Length == 0
            ? GetDefault(destination, context)
            : JsonSerializer.Deserialize<JsonElement?>(source);
    }

    public string? Convert(JsonElement? source, string? destination, ResolutionContext context)
    {
        return !source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined
            ? destination
            : JsonSerializer.Serialize(source);
    }

    public JsonElement? Convert(string? source, JsonElement? destination, ResolutionContext context)
    {
        return string.IsNullOrEmpty(source)
            ? GetDefault(destination, context)
            : JsonSerializer.Deserialize<JsonElement?>(source);
    }

    public JsonElement Convert(JsonElement source, JsonElement destination, ResolutionContext context)
    {
        return source.ValueKind == JsonValueKind.Undefined ? GetDefault(destination, context) : source;
    }

    public JsonElement? Convert(JsonElement? source, JsonElement? destination, ResolutionContext context)
    {
        return source.HasValue && source.Value.ValueKind != JsonValueKind.Undefined ? source : GetDefault(destination, context);
    }

    public JsonElement? Convert(JsonElement source, JsonElement? destination, ResolutionContext context)
    {
        return source.ValueKind == JsonValueKind.Undefined ? GetDefault(destination, context) : source;
    }

    public JsonElement Convert(JsonElement? source, JsonElement destination, ResolutionContext context)
    {
        return source.HasValue && source.Value.ValueKind != JsonValueKind.Undefined ? source.Value : GetDefault(destination, context);
    }
}