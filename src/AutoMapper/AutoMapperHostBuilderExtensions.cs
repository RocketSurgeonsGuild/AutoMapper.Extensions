using Rocket.Surgery.Conventions;
using Rocket.Surgery.Extensions.AutoMapper;

// ReSharper disable once CheckNamespace
namespace Rocket.Surgery.Conventions
{
    /// <summary>
    ///  AutoMapperHostBuilderExtensions.
    /// </summary>
    public static class AutoMapperHostBuilderExtensions
    {
        /// <summary>
        /// Uses AutoMapper.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="options">The options object</param>
        /// <returns>IConventionHostBuilder.</returns>
        public static IConventionHostBuilder UseAutoMapper(this IConventionHostBuilder container, AutoMapperOptions options = null)
        {
            container.Set(options ?? new AutoMapperOptions());
            container.Scanner.PrependConvention<AutoMapperConvention>();
            return container;
        }
    }
}
