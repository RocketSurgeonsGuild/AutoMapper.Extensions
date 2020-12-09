using System.IO;
using System.Text.Json;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Rocket.Surgery.Extensions.AutoMapper.NewtonsoftJson {
    static class ConverterHelpers
    {
        internal static byte[] WriteToBytes(JToken source)
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