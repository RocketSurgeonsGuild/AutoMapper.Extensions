using System.Text.Json;
using AutoMapper;

namespace Rocket.Surgery.Extensions.AutoMapper.SystemTextJson;

public class SystemJsonTextProfile : Profile
{
    public SystemJsonTextProfile()
    {
        CreateMap<JsonElement, byte[]?>().ConvertUsing(
            (source, destination) => source.ValueKind == JsonValueKind.Undefined
                ? destination
                : JsonSerializer.SerializeToUtf8Bytes(source)
        );
        CreateMap<JsonElement, string?>().ConvertUsing(
            (source, destination) => source.ValueKind == JsonValueKind.Undefined
                ? destination
                : JsonSerializer.Serialize(source)
        );
        CreateMap<JsonElement?, byte[]?>().ConvertUsing(
            (source, destination) => !source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined
                ? destination
                : JsonSerializer.SerializeToUtf8Bytes(source)
        );
        CreateMap<JsonElement?, string?>().ConvertUsing(
            (source, destination) => !source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined
                ? destination
                : JsonSerializer.Serialize(source)
        );
        CreateMap<byte[]?, JsonElement>().ConvertUsing(
            (source, destination) => source == null || source.Length == 0
                ? destination
                : JsonSerializer.Deserialize<JsonElement>(source)
        );
        CreateMap<string?, JsonElement>().ConvertUsing(
            (source, destination) => string.IsNullOrEmpty(source)
                ? destination
                : JsonSerializer.Deserialize<JsonElement>(source)
        );
        CreateMap<byte[]?, JsonElement?>().ConvertUsing(
            (source, destination) => source == null || source.Length == 0
                ? destination
                : JsonSerializer.Deserialize<JsonElement?>(source)
        );
        CreateMap<string?, JsonElement?>().ConvertUsing(
            (source, destination) => string.IsNullOrEmpty(source)
                ? destination
                : JsonSerializer.Deserialize<JsonElement?>(source)
        );
        CreateMap<JsonElement?, JsonElement>().ConvertUsing(
            (source, destination) => source.HasValue && source.Value.ValueKind != JsonValueKind.Undefined ? source.Value : destination
        );
        CreateMap<JsonElement, JsonElement?>().ConvertUsing((source, destination) => source.ValueKind == JsonValueKind.Undefined ? destination : source);
    }

    /// <summary>
    /// Gets the name of the profile.
    /// </summary>
    /// <value>The name of the profile.</value>
    public override string ProfileName => nameof(SystemJsonTextProfile);
}
