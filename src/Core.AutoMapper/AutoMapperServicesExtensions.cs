using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rocket.Surgery.Extensions.AutoMapper;
using Rocket.Surgery.Extensions.AutoMapper.Builders;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mapper = AutoMapper.Mapper;

namespace Rocket.Surgery.Extensions.AutoMapper
{
    public static class AutoMapperServicesExtensions
    {
        public static AutoMapperServicesBuilder WithAutoMapper(this IServiceConventionContext context)
        {
            AddAutoMapperClasses(context);
            return new AutoMapperServicesBuilder(context);
        }

        private static IServiceConventionContext AddAutoMapperClasses(IServiceConventionContext context)
        {
            if (context.Services.Any(z => z.ServiceType == typeof(IMapper))) return context;

            var assemblies = context.AssemblyCandidateFinder.GetCandidateAssemblies(nameof(AutoMapper));

            var allTypes = assemblies
                .Where(a => a.GetName().Name != nameof(AutoMapper))
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            context.Services.AddSingleton(new AutoMapperProfileContainer(allTypes
                .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray()));
            context.Services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IMappingAction<,>)
            };

            foreach (var type in openTypes.SelectMany(openType => allTypes
                .Where(t => t.IsClass
                    && !t.IsAbstract
                    && t.AsType().ImplementsGenericInterface(openType))))
            {
                context.Services.AddTransient(type.AsType());
            }

            context.Services.AddSingleton<IConfigurationProvider>(_ => new MapperConfiguration(ConfigAction(_)));

            return context;
        }

        internal static Action<IMapperConfigurationExpression> ConfigAction(IServiceProvider serviceProvider)
        {
            return cfg =>
            {
                cfg.ConstructServicesUsing(serviceProvider.GetService);

                var profiles = serviceProvider.GetServices<AutoMapperProfileContainer>()
                    .SelectMany(x => x.Profiles)
                    .Select(z => z.AsType())
                    .Distinct()
                    .ToArray();
                foreach (var profile in profiles)
                    cfg.AddProfile(profile);

                foreach (var @delegate in serviceProvider.GetServices<AutoMapperConfigurationDelegateContainer>().Select(x => x.Delegate))
                {
                    if (@delegate is AutoMapperConfigurationDelegate configurationDelegate)
                        configurationDelegate(cfg);
                    if (@delegate is AutoMapperComponentConfigurationDelegate componentConfigurationDelegate)
                        componentConfigurationDelegate(serviceProvider, cfg);
                }
            };
        }

        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            return type.IsGenericType(interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => @interface.IsGenericType(interfaceType));
        }

        private static bool IsGenericType(this Type type, Type genericType)
            => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}
