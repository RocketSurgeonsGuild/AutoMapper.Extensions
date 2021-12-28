using System.Reflection;
using AutoMapper;

namespace Rocket.Surgery.Extensions.AutoMapper;

internal static class Helpers
{
    private static readonly FieldInfo _field = typeof(ResolutionContext).GetField("_inner", BindingFlags.Instance | BindingFlags.NonPublic);
    public static AutoMapperLogger? GetLogger(ResolutionContext context)
    {
        var field = _field.GetValue(context);
        if (field == null || !(field is IMapper mapper))
        {
            return null;
        }
        return mapper.ConfigurationProvider.Features?.Get<AutoMapperLogger>();
    }
}