using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rocket.Surgery.Extensions.AutoMapper;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Reflection;
using Rocket.Surgery.Extensions.DependencyInjection;
using Rocket.Surgery.Conventions.Reflection;

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
        public AutoMapperConvention(AutoMapperOptions options = null)
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

            var allTypes = assemblies
                .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            if (_options.AutoLoadFromAssemblies)
            {
                context.Services.Configure<MapperConfigurationExpression>(options =>
                {
                    options.AddMaps(assemblies);
                });
            }

            context.Services.Add(ServiceDescriptor.Describe(
                typeof(IMapper),
                _ => new Mapper(_.GetRequiredService<IConfigurationProvider>()),
                _options.ServiceLifetime)
            );

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IValueConverter<,>),
                typeof(IMappingAction<,>)
            };

            foreach (var type in openTypes.SelectMany(openType => allTypes
                .Where(t => t.IsClass
                            && !t.IsAbstract
                            && ImplementsGenericInterface(t.AsType(), openType))))
            {
                context.Services.AddTransient(type.AsType());
            }

            context.Services.AddSingleton<IConfigurationProvider>(_ =>
            {
                var options = _.GetRequiredService<IOptions<MapperConfigurationExpression>>();
                options.Value.ConstructServicesUsing(_.GetService);
                return new MapperConfiguration(options.Value);
            });
        }
        private static bool ImplementsGenericInterface(Type type, Type interfaceType)
        {
            return IsGenericType(type, interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => IsGenericType(@interface, interfaceType));
        }

        private static bool IsGenericType(Type type, Type genericType)
            => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}
