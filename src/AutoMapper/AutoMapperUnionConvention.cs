using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Extensions.AutoMapper;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Extensions.DependencyInjection;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Unions;

[assembly: Convention(typeof(AutoMapperUnionConvention))]

namespace Rocket.Surgery.Extensions.AutoMapper
{
    /// <summary>
    /// Convention to register unions that are tracked by <see cref="UnionAttribute" />
    /// </summary>
    /// <seealso cref="Rocket.Surgery.Extensions.DependencyInjection.IServiceConvention" />
    public class AutoMapperUnionConvention : IServiceConvention
    {
        /// <summary>
        /// Registers the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Register(IServiceConventionContext context)
        {
            var assemblies = context.AssemblyCandidateFinder.GetCandidateAssemblies(typeof(UnionAttribute).Assembly.GetName().Name);

            context.Services.Configure<MapperConfigurationExpression>(options =>
            {
                options.MapUnions(assemblies);
            });
        }
    }
}
