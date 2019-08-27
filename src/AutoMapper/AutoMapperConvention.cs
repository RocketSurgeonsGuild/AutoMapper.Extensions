using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rocket.Surgery.Extensions.AutoMapper;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Extensions.DependencyInjection;
using Rocket.Surgery.Conventions.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

[assembly: Convention(typeof(AutoMapperConvention))]

namespace Rocket.Surgery.Extensions.AutoMapper
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
        /// Initializes a new instance of the <see cref="AutoMapperConvention"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AutoMapperConvention(AutoMapperOptions? options = null)
        {
            _options = options ?? new AutoMapperOptions();
        }

        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Register(IServiceConventionContext context)
        {
            AddAutoMapperClasses(context);
        }

        private void AddAutoMapperClasses(IServiceConventionContext context)
        {
            var assemblies = context.AssemblyCandidateFinder.GetCandidateAssemblies(nameof(AutoMapper)).ToArray();
            context.Services.AddAutoMapper(assemblies, _options.ServiceLifetime);
            context.Services.Replace(ServiceDescriptor.Singleton<IConfigurationProvider>(_ =>
            {
                var options = _.GetRequiredService<IOptions<MapperConfigurationExpression>>();
                options.Value.AddMaps(assemblies);
                return new MapperConfiguration(options.Value);
            }));
        }
    }
}
