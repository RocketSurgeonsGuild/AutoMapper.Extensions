using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    /// <summary>
    /// Class AutoMapperServicesBuilderExtensions.
    /// </summary>
    public static class AutoMapperServicesBuilderExtensions
    {
        /// <summary>
        /// Configures the specified action.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="action">The action.</param>
        /// <returns>AutoMapperServicesBuilder.</returns>
        public static AutoMapperServicesBuilder Configure(this AutoMapperServicesBuilder builder, AutoMapperConfigurationDelegate action)
        {
            builder.AddDelegate(action);
            return builder;
        }

        /// <summary>
        /// Configures the specified action.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="action">The action.</param>
        /// <returns>AutoMapperServicesBuilder.</returns>
        public static AutoMapperServicesBuilder Configure(this AutoMapperServicesBuilder builder, AutoMapperComponentConfigurationDelegate action)
        {
            builder.AddDelegate(action);
            return builder;
        }

        /// <summary>
        /// Withes the profile.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="profile">The profile.</param>
        /// <returns>AutoMapperServicesBuilder.</returns>
        public static AutoMapperServicesBuilder WithProfile(this AutoMapperServicesBuilder builder, Type profile)
        {
            builder.AddProfile(new[] { profile });
            return builder;
        }

        /// <summary>
        /// Withes the profile.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="profile">The profile.</param>
        /// <returns>AutoMapperServicesBuilder.</returns>
        public static AutoMapperServicesBuilder WithProfile(this AutoMapperServicesBuilder builder, TypeInfo profile)
        {
            builder.AddProfile(new[] { profile });
            return builder;
        }
    }
}
