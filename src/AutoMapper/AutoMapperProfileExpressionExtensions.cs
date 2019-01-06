using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.Mappers;
using Rocket.Surgery.Unions;

namespace Rocket.Surgery.Extensions.AutoMapper
{
    public static class AutoMapperProfileExpressionExtensions
    {
        public static T OnlyDefinedProperties<T>(this T configuration)
            where T : IProfileExpression
        {
            configuration.AllowNullDestinationValues = configuration.AllowNullDestinationValues ?? true;
            configuration.AllowNullCollections = configuration.AllowNullCollections ?? true;
            configuration.ForAllPropertyMaps(
                OnlyDefinedPropertiesMethods.ForStrings,
                OnlyDefinedPropertiesMethods.StringCondition);
            configuration.ForAllPropertyMaps(
                OnlyDefinedPropertiesMethods.ForValueTypes,
                OnlyDefinedPropertiesMethods.ValueTypeCondition);
            configuration.ForAllPropertyMaps(
                OnlyDefinedPropertiesMethods.ForNullableValueTypes,
                OnlyDefinedPropertiesMethods.NullableValueTypeCondition);
            return configuration;
        }

        public static T MapWithPostfix<T>(
            this T configuration,
            string sourcePostFix,
            string destinationPostFix,
            MemberList memberList,
            bool ignoreInaccessibleProperties = true
        ) where T : IProfileExpression
        {
            configuration
                .AddMemberConfiguration()
                .AddName<PrePostfixName>(_ =>
                {
                    _.AddStrings(p => p.Postfixes, sourcePostFix);
                    _.AddStrings(p => p.DestinationPostfixes, destinationPostFix);
                });

            configuration
                .AddConditionalObjectMapper()
                .Where(PostfixCondition(sourcePostFix, destinationPostFix));

            configuration.ForAllMaps((map, expression) =>
            {
                expression.ValidateMemberList(memberList);
                if (ignoreInaccessibleProperties)
                {
                    if (memberList == MemberList.Source)
                    {
                        expression.IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
                    }
                    else if (memberList == MemberList.Destination)
                    {
                        expression.IgnoreAllPropertiesWithAnInaccessibleSetter();
                    }
                }
            });

            return configuration;
        }

        public static T MapDtoToModel<T>(this T configuration, string modelPostfix = null)
            where T : IProfileExpression
        {
            configuration.AllowNullCollections = false;
            configuration.AllowNullDestinationValues = false;
            MapWithPostfix(configuration, "Dto", modelPostfix ?? "Model", MemberList.Destination, false);

            return configuration;
        }

        public static T MapModelToDto<T>(this T configuration, string modelPostfix = null)
            where T : IProfileExpression
        {
            configuration.MapWithPostfix(modelPostfix ?? "Model", "Dto", MemberList.Source);
            configuration.OnlyDefinedProperties();

            return configuration;
        }

        public static T MapUnions<T>(this T profile)
           where T : IProfileExpression
        {
            profile.ForAllMaps((map, expression) =>
            {
                if (map.SourceType == null || map.DestinationType == null) return;

                var sourceRootType = UnionHelper.GetRootType(map.SourceType);
                var destinationRootType = UnionHelper.GetRootType(map.DestinationType);
                if (sourceRootType == null || destinationRootType == null) return;

                var sourceEnumType = UnionHelper.GetUnionEnumType(sourceRootType);
                var destinationEnumType = UnionHelper.GetUnionEnumType(destinationRootType);
                if (sourceEnumType != destinationEnumType) return;

                expression.ForMember(
                    sourceRootType.GetCustomAttribute<UnionKeyAttribute>(true).Key,
                    x => x.Ignore()
                );

                if (sourceRootType.AsType() == map.SourceType && destinationRootType.AsType() == map.DestinationType)
                {
                    var sourceUnion = UnionHelper.GetUnion(sourceRootType);
                    var destinationUnion = UnionHelper.GetUnion(destinationRootType);

                    var invertedSourceUnion = sourceUnion.ToDictionary(x => x.Value, x => x.Key);
                    expression.ConstructUsing((src, context) =>
                    {
                        return context.Mapper.Map(src, src.GetType(), destinationUnion[invertedSourceUnion[src.GetType()]]);
                    });
                }
            });

            return profile;
        }

        private static Func<Type, Type, bool> PostfixCondition(string sourcePostFix, string destinationPostFix)
        {
            return (source, destination) =>
            {
                if (!source.Name.EndsWith(sourcePostFix)) return false;
                if (!destination.Name.EndsWith(destinationPostFix)) return false;

                var sourceName = source.Name.Substring(0, source.Name.IndexOf(sourcePostFix));
                var destinationName = destination.Name.Substring(0, destination.Name.IndexOf(destinationPostFix));

                return sourceName == destinationName;
            };
        }
    }

    static class OnlyDefinedPropertiesMethods
    {
        public static bool ForStrings(PropertyMap map)
        {
            if (map.HasSource && map.SourceType == typeof(string) && map.DestinationType == typeof(string))
            {
                return true;
            }
            return false;
        }

        public static void StringCondition(PropertyMap map, IMemberConfigurationExpression expression)
        {
            expression.Condition((source, destination, sourceValue, sourceDestination, context) =>
            {
                if (!string.IsNullOrWhiteSpace((string)sourceValue))
                {
                    return true;
                }
                return false;
            });
        }

        public static bool ForValueTypes(PropertyMap map)
        {
            if (!map.HasSource) return false;
            var source = map.SourceType.GetTypeInfo();
            var destination = map.DestinationType.GetTypeInfo();
            if (source != null && !source.IsEnum && source.IsValueType && destination.IsValueType)
            {
                return true;
            }
            return false;
        }

        public static void ValueTypeCondition(PropertyMap map, IMemberConfigurationExpression expression)
        {
            var defaultValue = Activator.CreateInstance(map.SourceType);
            expression.Condition((source, destination, sourceValue, sourceDestination, context) =>
            {
                if (!defaultValue.Equals(sourceValue))
                {
                    return true;
                }
                return false;
            });
        }

        public static bool ForNullableValueTypes(PropertyMap map)
        {
            if (!map.HasSource) return false;
            var source = Nullable.GetUnderlyingType(map.SourceType)?.GetTypeInfo();
            var destination = Nullable.GetUnderlyingType(map.DestinationType)?.GetTypeInfo();
            if (source == null || destination == null)
            {
                return false;
            }

            if (!source.IsEnum && source.IsValueType && destination.IsValueType)
            {
                return true;
            }

            return false;
        }

        public static void NullableValueTypeCondition(PropertyMap map, IMemberConfigurationExpression expression)
        {
            expression.Condition((source, destination, sourceValue, sourceDestination, context) =>
            {
                if (sourceValue != null)
                {
                    return true;
                }

                return false;
            });
        }
    }
}
