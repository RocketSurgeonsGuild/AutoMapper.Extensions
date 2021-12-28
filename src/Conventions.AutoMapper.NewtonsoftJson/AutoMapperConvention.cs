using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.AutoMapper.NewtonsoftJson;
using Rocket.Surgery.Conventions.DependencyInjection;

[assembly: Convention(typeof(AutoMapperNewtonsoftJsonConvention))]

namespace Rocket.Surgery.Conventions.AutoMapper.NewtonsoftJson;

/// <summary>
/// AutoMapperConvention.
/// Implements the <see cref="IServiceConvention" />
/// </summary>
/// <seealso cref="IServiceConvention" />
[DependsOnConvention(typeof(AutoMapperConvention))]
public class AutoMapperNewtonsoftJsonConvention : IServiceConvention
{
    /// <summary>
    /// Registers the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="configuration"></param>
    /// <param name="services"></param>
    public void Register(IConventionContext context, Microsoft.Extensions.Configuration.IConfiguration configuration, IServiceCollection services)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
    }
}
