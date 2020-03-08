using System;
using JetBrains.Annotations;
using Rocket.Surgery.Extensions.AutoMapper;

// ReSharper disable once CheckNamespace
namespace Rocket.Surgery.Conventions
{
    /// <summary>
    /// AutoMapperHostBuilderExtensions.
    /// </summary>
    [PublicAPI]
    public static class AutoMapperHostBuilderExtensions
    {
        /// <summary>
        /// Uses AutoMapper.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="options">The options object</param>
        /// <returns>IConventionHostBuilder.</returns>
        public static IConventionHostBuilder UseAutoMapper(
            [NotNull] this IConventionHostBuilder container,
            AutoMapperOptions? options = null
        )
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.Set(options ?? new AutoMapperOptions());
            container.Scanner.PrependConvention<AutoMapperConvention>();
            return container;
        }

        // /// <summary>
        // /// Uses AutoMapper.
        // /// </summary>
        // /// <param name="container">The container.</param>
        // /// <returns>IConventionHostBuilder.</returns>
        // public static IConventionHostBuilder UseAutoMapperUnions(this IConventionHostBuilder container)
        // {
        //     UseAutoMapper(container);
        //     container.Scanner.PrependConvention<AutoMapperUnionConvention>();
        //     return container;
        // }
    }
}