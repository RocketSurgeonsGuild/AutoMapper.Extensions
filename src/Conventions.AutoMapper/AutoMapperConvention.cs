using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.AutoMapper;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Extensions.AutoMapper;

[assembly: Convention(typeof(AutoMapperConvention))]

namespace Rocket.Surgery.Conventions.AutoMapper
{
    /// <summary>
    /// AutoMapperConvention.
    /// Implements the <see cref="IServiceConvention" />
    /// </summary>
    /// <seealso cref="IServiceConvention" />
    public class AutoMapperConvention : IServiceConvention
    {
        private readonly AutoMapperOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperConvention" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AutoMapperConvention(AutoMapperOptions? options = null) => _options = options ?? new AutoMapperOptions();

        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configuration"></param>
        /// <param name="services"></param>
        public void Register([NotNull] IConventionContext context, Microsoft.Extensions.Configuration.IConfiguration configuration, IServiceCollection services)
        {
            var assemblies = context.AssemblyCandidateFinder.GetCandidateAssemblies(nameof(AutoMapper)).ToArray();
            services.AddAutoMapper(
                (_, expression) =>
                {
                    expression.Features.Set(_options);
                    expression.Features.Set(
                        new AutoMapperLogger(
                            _.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(AutoMapperLogger))
                        )
                    );
                },
                assemblies,
                _options.ServiceLifetime
            );
        }
    }
}