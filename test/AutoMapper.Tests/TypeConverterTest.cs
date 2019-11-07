using System;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper;
using Bogus;
using Bogus.Extensions;
using Microsoft.CodeAnalysis;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public abstract class TypeConverterTest<T>
    {
        public static IEnumerable<Type> GetAllTypeConverters() => typeof(T).GetInterfaces()
                .Where(x => x.IsGenericType && typeof(ITypeConverter<,>).IsAssignableFrom(x.GetGenericTypeDefinition()));
        public static IEnumerable<(Type source, Type destination)> GetValueTypePairs()
            => GetAllTypeConverters()
                .Select(x => (
                    source: Nullable.GetUnderlyingType(x.GetGenericArguments()[0]) ?? x.GetGenericArguments()[0],
                    destination: Nullable.GetUnderlyingType(x.GetGenericArguments()[1]) ?? x.GetGenericArguments()[1])
                )
                .Where(x => x.source.IsValueType && x.destination.IsValueType)
                .Distinct();
        public static readonly Faker Faker = new Faker();
        public static object GetRandomValue(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type == typeof(int))
            {
                return Faker.Random.Int();
            }
            if (type == typeof(long))
            {
                return Faker.Random.Long();
            }
            if (type == typeof(short))
            {
                return Faker.Random.Short();
            }
            if (type == typeof(float))
            {
                return Faker.Random.Float();
            }
            if (type == typeof(double))
            {
                return Faker.Random.Double();
            }
            if (type == typeof(decimal))
            {
                return Faker.Random.Decimal();
            }
            if (type == typeof(Duration))
            {
                return Duration.FromTimeSpan(Faker.Date.Timespan());
            }
            if (type == typeof(TimeSpan))
            {
                return Faker.Date.Timespan(TimeSpan.FromDays(1));
            }
            if (type == typeof(Instant))
            {
                return Instant.FromDateTimeOffset(Faker.Date.RecentOffset());
            }
            if (type == typeof(LocalDateTime))
            {
                return LocalDateTime.FromDateTime(Faker.Date.Recent());
            }
            if (type == typeof(OffsetDateTime))
            {
                return OffsetDateTime.FromDateTimeOffset(Faker.Date.RecentOffset());
            }
            if (type == typeof(LocalTime))
            {
                return LocalTime.FromTicksSinceMidnight(Faker.Date.Timespan(TimeSpan.FromDays(1)).Ticks);
            }
            if (type == typeof(LocalDate))
            {
                return LocalDate.FromDateTime(Faker.Date.Recent());
            }
            if (type == typeof(Offset))
            {
                return Offset.FromTimeSpan(Faker.Date.Timespan(TimeSpan.FromHours(12)));
            }
            if (type == typeof(DateTime))
            {
                return Faker.Date.Recent();
            }
            if (type == typeof(DateTimeOffset))
            {
                return Faker.Date.RecentOffset();
            }
            throw new NotSupportedException($"type {type.FullName} is not supported");
        }

        public TypeConverterTest()
        {
            _config = new MapperConfiguration(x =>
            {
                x.AddProfile<NodaTimeProfile>();
                foreach (var (source, destination) in GetValueTypePairs().SelectMany(item => new[] {
                    item,
                    (typeof(Nullable<>).MakeGenericType(item.source), typeof(Nullable<>).MakeGenericType(item.destination)),
                    (item.source, typeof(Nullable<>).MakeGenericType(item.destination))
                }))
                {
                    x.CreateMap(typeof(Foo<>).MakeGenericType(source), typeof(Foo<>).MakeGenericType(destination));
                }
                Configure(x);
            });
            _mapper = _config.CreateMapper();
        }

        protected abstract void Configure(IMapperConfigurationExpression expression);

        public static IEnumerable<object?[]> GetTestCases()
        {
            static (Type source, Type sourceClass, Type destination, Type destinationClass) GetWrappedClasses((Type source, Type destination) item)
            {
                var (source, destination) = item;
                var sourceFoo = typeof(Foo<>).MakeGenericType(source);
                var destinationFoo = typeof(Foo<>).MakeGenericType(destination);
                return (source, sourceFoo, destination, destinationFoo);
            }

            static object CreateValue(Type type, object value)
            {
                return typeof(Foo)
                    .GetMethod(nameof(Foo.Create))!
                    .MakeGenericMethod(type).Invoke(null, new object[] { value })!;
            }

            foreach (var (source, sourceClass, destination, destinationClass) in GetValueTypePairs()
                .SelectMany(item => new[] {
                    item,
                    (typeof(Nullable<>).MakeGenericType(item.source), typeof(Nullable<>).MakeGenericType(item.destination)),
                    (item.source, typeof(Nullable<>).MakeGenericType(item.destination))
                }).Select(GetWrappedClasses))
            {
                var sourceValue = CreateValue(source, GetRandomValue(source));
                yield return new object?[] { sourceClass, destinationClass, sourceValue };

                if (Nullable.GetUnderlyingType(source) == null) continue;

                foreach (var item in Faker.Make(3, () => CreateValue(source, GetRandomValue(source).OrNull(Faker))))
                {
                    yield return new object?[] {
                        sourceClass,
                        destinationClass,
                        item
                    };
                }
            }
        }




        protected readonly IMapper _mapper;
        protected readonly MapperConfiguration _config;


    }
}
