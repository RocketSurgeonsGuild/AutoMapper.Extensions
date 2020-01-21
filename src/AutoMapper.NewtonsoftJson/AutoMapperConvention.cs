using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Features;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Extensions.AutoMapper.NewtonsoftJson;
using Rocket.Surgery.Extensions.DependencyInjection;

[assembly: Convention(typeof(AutoMapperNewtonsoftJsonConvention))]

namespace Rocket.Surgery.Extensions.AutoMapper.NewtonsoftJson
{
    /// <summary>
    /// AutoMapperConvention.
    /// Implements the <see cref="IServiceConvention" />
    /// </summary>
    /// <seealso cref="IServiceConvention" />
    [DependsOnConvention(typeof(AutoMapperConvention))]
    public class AutoMapperNewtonsoftJsonConvention : IServiceConvention
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperNewtonsoftJsonConvention" /> class.
        /// </summary>
        public AutoMapperNewtonsoftJsonConvention() { }

        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Register([NotNull] IServiceConventionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Services.Configure<MapperConfigurationExpression>(expression => expression.AddProfile(new NewtonsoftJsonProfile()));
        }
    }
}