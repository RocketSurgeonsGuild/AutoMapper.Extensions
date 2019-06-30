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
    /// <summary>
    /// AutoMapperServicesBuilder.
    /// Implements the <see cref="Rocket.Surgery.Builders.Builder{Rocket.Surgery.Extensions.DependencyInjection.IServiceConventionContext}" />
    /// </summary>
    /// <seealso cref="Rocket.Surgery.Builders.Builder{Rocket.Surgery.Extensions.DependencyInjection.IServiceConventionContext}" />
    public partial class AutoMapperServicesBuilder : Builder<IServiceConventionContext>
    {
        private readonly IServiceCollection _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperServicesBuilder"/> class.
        /// </summary>
        /// <param name="servicesBuilder">The services builder.</param>
        internal AutoMapperServicesBuilder(IServiceConventionContext servicesBuilder) : base(servicesBuilder, servicesBuilder.Properties)
        {
            _services = servicesBuilder.Services;
        }

        /// <summary>
        /// Adds the delegate.
        /// </summary>
        /// <param name="delegate">The delegate.</param>
        internal void AddDelegate(AutoMapperComponentConfigurationDelegate @delegate)
        {
            _services.AddSingleton(new AutoMapperConfigurationDelegateContainer(@delegate));
        }

        /// <summary>
        /// Adds the delegate.
        /// </summary>
        /// <param name="delegate">The delegate.</param>
        internal void AddDelegate(AutoMapperConfigurationDelegate @delegate)
        {
            _services.AddSingleton(new AutoMapperConfigurationDelegateContainer(@delegate));
        }

        /// <summary>
        /// Adds the profile.
        /// </summary>
        /// <param name="profileTypes">The profile types.</param>
        internal void AddProfile(IEnumerable<Type> profileTypes)
        {
            _services.AddSingleton(new AutoMapperProfileContainer(profileTypes.Select(x => x.GetTypeInfo())));
        }

        /// <summary>
        /// Adds the profile.
        /// </summary>
        /// <param name="profileTypes">The profile types.</param>
        internal void AddProfile(IEnumerable<TypeInfo> profileTypes)
        {
            _services.AddSingleton(new AutoMapperProfileContainer(profileTypes));
        }

        /// <summary>
        /// Uses the static registration.
        /// </summary>
        /// <returns>AutoMapperServicesBuilder.</returns>
        public AutoMapperServicesBuilder UseStaticRegistration()
        {
            Parent.Services.Replace(ServiceDescriptor.Singleton(_ => Mapper.Configuration));
            Parent.OnBuild.Subscribe(new BuildObservable());
            return this;
        }
    }
}

