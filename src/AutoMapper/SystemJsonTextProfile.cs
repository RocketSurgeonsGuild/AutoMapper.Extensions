using System.Text.Json;
using AutoMapper;

namespace Rocket.Surgery.Extensions.AutoMapper
{
    public class SystemJsonTextProfile : Profile
    {
        public SystemJsonTextProfile()
        {
            var converter = new JsonElementConverter();
            CreateMap<JsonElement, byte[]?>().ConvertUsing(converter);
            CreateMap<JsonElement, string?>().ConvertUsing(converter);
            CreateMap<JsonElement?, byte[]?>().ConvertUsing(converter);
            CreateMap<JsonElement?, string?>().ConvertUsing(converter);
            CreateMap<byte[]?, JsonElement>().ConvertUsing(converter);
            CreateMap<string?, JsonElement>().ConvertUsing(converter);
            CreateMap<byte[]?, JsonElement?>().ConvertUsing(converter);
            CreateMap<string?, JsonElement?>().ConvertUsing(converter);
        }
    }
}