using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;

namespace Rocket.Surgery.Extensions.AutoMapper.Builders
{
    public static class AutoMapperServicesBuilderExtensions
    {
        public static AutoMapperServicesBuilder Configure(this AutoMapperServicesBuilder builder, AutoMapperConfigurationDelegate action)
        {
            builder.AddDelegate(action);
            return builder;
        }

        public static AutoMapperServicesBuilder Configure(this AutoMapperServicesBuilder builder, AutoMapperComponentConfigurationDelegate action)
        {
            builder.AddDelegate(action);
            return builder;
        }

        public static AutoMapperServicesBuilder WithProfile(this AutoMapperServicesBuilder builder, Type profile)
        {
            builder.AddProfile(new[] { profile });
            return builder;
        }

        public static AutoMapperServicesBuilder WithProfile(this AutoMapperServicesBuilder builder, TypeInfo profile)
        {
            builder.AddProfile(new[] { profile });
            return builder;
        }
    }
}
