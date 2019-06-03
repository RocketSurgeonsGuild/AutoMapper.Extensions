using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rocket.Surgery.Extensions.DependencyInjection;
using Rocket.Surgery.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    public partial class AutoMapperServicesBuilder : Builder<IServiceConventionContext>
    {
        private readonly IServiceCollection _services;

        internal AutoMapperServicesBuilder(IServiceConventionContext servicesBuilder) : base(servicesBuilder, servicesBuilder.Properties)
        {
            _services = servicesBuilder.Services;
        }

        internal void AddDelegate(AutoMapperComponentConfigurationDelegate @delegate)
        {
            _services.AddSingleton(new AutoMapperConfigurationDelegateContainer(@delegate));
        }

        internal void AddDelegate(AutoMapperConfigurationDelegate @delegate)
        {
            _services.AddSingleton(new AutoMapperConfigurationDelegateContainer(@delegate));
        }

        internal void AddProfile(IEnumerable<Type> profileTypes)
        {
            _services.AddSingleton(new AutoMapperProfileContainer(profileTypes.Select(x => x.GetTypeInfo())));
        }

        internal void AddProfile(IEnumerable<TypeInfo> profileTypes)
        {
            _services.AddSingleton(new AutoMapperProfileContainer(profileTypes));
        }

        public AutoMapperServicesBuilder UseStaticRegistration()
        {
            Parent.Services.Replace(ServiceDescriptor.Singleton(_ => Mapper.Configuration));
            Parent.OnBuild.Subscribe(new BuildObservable());
            return this;
        }
    }
}

