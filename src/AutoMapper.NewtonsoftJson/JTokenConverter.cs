using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;
using JsonException = System.Text.Json.JsonException;

namespace Rocket.Surgery.Extensions.AutoMapper.NewtonsoftJson
{
    public class JTokenConverter :
        ITypeConverter<JToken, byte[]?>,
        ITypeConverter<byte[]?, JToken?>,
        ITypeConverter<JToken?, string?>,
        ITypeConverter<string?, JToken?>,
        ITypeConverter<JsonElement?, JToken?>,
        ITypeConverter<JToken?, JsonElement?>,
        ITypeConverter<JsonElement, JToken?>,
        ITypeConverter<JToken?, JsonElement>,
        ITypeConverter<JArray?, byte[]?>,
        ITypeConverter<byte[]?, JArray?>,
        ITypeConverter<JArray?, string?>,
        ITypeConverter<string?, JArray?>,
        ITypeConverter<JsonElement?, JArray?>,
        ITypeConverter<JArray?, JsonElement?>,
        ITypeConverter<JsonElement, JArray?>,
        ITypeConverter<JArray?, JsonElement>,
        ITypeConverter<JObject?, byte[]?>,
        ITypeConverter<byte[]?, JObject?>,
        ITypeConverter<JObject?, string?>,
        ITypeConverter<string?, JObject?>,
        ITypeConverter<JsonElement?, JObject?>,
        ITypeConverter<JObject?, JsonElement?>,
        ITypeConverter<JsonElement, JObject?>,
        ITypeConverter<JObject?, JsonElement>
    {
        private static readonly JsonElement _empty = JsonSerializer.Deserialize<JsonElement>("null");

        private static JsonDefaultValue GetJsonDefaultValue(ResolutionContext context) => JsonDefaultValue.NotNull;
        private static JsonElement GetDefaultSjt(JsonElement value, ResolutionContext context) => GetJsonDefaultValue(context) switch
        {
            JsonDefaultValue.NotNull => value.ValueKind == JsonValueKind.Undefined ? _empty : value,
            _ => value
        };
        private static JsonElement? GetDefaultSjt(JsonElement? value, ResolutionContext context) => GetJsonDefaultValue(context) switch
        {
            JsonDefaultValue.NotNull => !value.HasValue || value.Value.ValueKind == JsonValueKind.Undefined ? _empty : value.Value,
            _ => value ?? default
        };
        private static JToken? GetDefaultToken(JToken? value, ResolutionContext context) => GetJsonDefaultValue(context) switch
        {
            JsonDefaultValue.NotNull => value ?? JValue.CreateNull(),
            _ => value ?? default
        };
        private static T? GetDefault<T>(T? value, ResolutionContext context) where T : JToken, new() => GetJsonDefaultValue(context) switch
        {
            JsonDefaultValue.NotNull => value ?? new T(),
            _ => value
        };

        public byte[]? Convert(JToken? source, byte[]? destination, ResolutionContext context)
        {
            if (source == null || source.Type == JTokenType.None)
                return destination ?? Array.Empty<byte>();
            return WriteToBytes(source);
        }


        public JToken? Convert(byte[]? source, JToken? destination, ResolutionContext context)
        {
            try
            {
                return source == null || source.Length == 0
                    ? destination
                    : JToken.Parse(Encoding.UTF8.GetString(source));
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination, context);
            }
        }


        public string? Convert(JToken? source, string? destination, ResolutionContext context)
            => source?.ToString(Formatting.None) ?? destination;


        public JToken? Convert(string? source, JToken? destination, ResolutionContext context)
        {
            try
            {
                return string.IsNullOrEmpty(source) ? destination : JToken.Parse(source);
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination, context);
            }
        }

        public byte[]? Convert(JArray? source, byte[]? destination, ResolutionContext context)
        {
            if (source == null || source.Type == JTokenType.None)
                return destination ?? Array.Empty<byte>();
            return WriteToBytes(source);
        }

        public JArray? Convert(byte[]? source, JArray? destination, ResolutionContext context)
        {
            try
            {
                return source == null || source.Length == 0
                    ? destination ?? new JArray()
                    : JArray.Parse(Encoding.UTF8.GetString(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public string? Convert(JArray? source, string? destination, ResolutionContext context)
            => source?.ToString(Formatting.None) ?? destination;

        public JArray? Convert(string? source, JArray? destination, ResolutionContext context)
        {
            try
            {
                return string.IsNullOrEmpty(source) ? destination ?? new JArray() : JArray.Parse(source);
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public byte[]? Convert(JObject? source, byte[]? destination, ResolutionContext context)
        {
            if (source == null || source.Type == JTokenType.None)
                return destination ?? Array.Empty<byte>();
            return WriteToBytes(source);
        }

        public JObject? Convert(byte[]? source, JObject? destination, ResolutionContext context)
        {
            try
            {
                return source == null || source.Length == 0
                    ? destination ?? new JObject()
                    : JObject.Parse(Encoding.UTF8.GetString(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public string? Convert(JObject? source, string? destination, ResolutionContext context)
            => source?.ToString(Formatting.None) ?? destination;

        public JObject? Convert(string? source, JObject? destination, ResolutionContext context)
        {
            try
            {
                return string.IsNullOrEmpty(source) ? destination ?? new JObject() : JObject.Parse(source);
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public JsonElement Convert(JObject? source, JsonElement destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination, context);
            }
            var data = WriteToBytes(source);
            try
            {
                var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination, context);
            }
        }

        public JObject? Convert(JsonElement source, JObject? destination, ResolutionContext context)
        {
            if (source.ValueKind == JsonValueKind.Undefined || source.ValueKind == JsonValueKind.Null) return GetDefault(destination, context);
            try
            {
                return JObject.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public JsonElement? Convert(JObject? source, JsonElement? destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination, context);
            }
            var data = WriteToBytes(source);
            try
            {
                var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination, context);
            }
        }

        public JObject? Convert(JsonElement? source, JObject? destination, ResolutionContext context)
        {
            if (!source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined || source.Value.ValueKind == JsonValueKind.Null) return  GetDefault(destination, context);
            try
            {
                return JObject.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public JsonElement Convert(JArray? source, JsonElement destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination, context);
            }
            var data = WriteToBytes(source);
            try
            {
                var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination, context);
            }
        }

        public JArray? Convert(JsonElement source, JArray? destination, ResolutionContext context)
        {
            if (source.ValueKind == JsonValueKind.Undefined || source.ValueKind == JsonValueKind.Null) return GetDefault(destination, context);
            try
            {
                return JArray.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public JsonElement? Convert(JArray? source, JsonElement? destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination, context);
            }
            var data = WriteToBytes(source);
            try
            {
                var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination, context);
            }
        }

        public JArray? Convert(JsonElement? source, JArray? destination, ResolutionContext context)
        {
            if (!source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined || source.Value.ValueKind == JsonValueKind.Null) return GetDefault(destination, context);
            try
            {
                return JArray.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefault(destination, context);
            }
        }

        public JsonElement Convert(JToken? source, JsonElement destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination, context);
            }
            var data = WriteToBytes(source);
            try
            {
                var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination, context);
            }
        }

        public JToken? Convert(JsonElement source, JToken? destination, ResolutionContext context)
        {
            if (source.ValueKind == JsonValueKind.Undefined || source.ValueKind == JsonValueKind.Null) return GetDefaultToken(destination, context);
            try
            {
                return JToken.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination, context);
            }
        }

        public JsonElement? Convert(JToken? source, JsonElement? destination, ResolutionContext context)
        {
            if (source is null)
            {
                return GetDefaultSjt(destination, context);
            }
            var data = WriteToBytes(source);
            try
            {
                var document = JsonDocument.Parse(data);
                return document.RootElement;
            }
            catch (JsonException)
            {
                return GetDefaultSjt(destination, context);
            }
        }

        public JToken? Convert(JsonElement? source, JToken? destination, ResolutionContext context)
        {
            if (!source.HasValue || source.Value.ValueKind == JsonValueKind.Undefined || source.Value.ValueKind == JsonValueKind.Null) return GetDefaultToken(destination, context);
            try
            {
                return JToken.Parse(JsonSerializer.Serialize(source));
            }
            catch (JsonReaderException)
            {
                return GetDefaultToken(destination, context);
            }
        }

        private byte[] WriteToBytes(JToken source)
        {
            using var memory = new MemoryStream();
            using var sw = new StreamWriter(memory);
            using var jw = new JsonTextWriter(sw) { Formatting = Formatting.None };
            source.WriteTo(jw);
            jw.Flush();
            memory.Position = 0;
            return memory.ToArray();
        }
    }
}