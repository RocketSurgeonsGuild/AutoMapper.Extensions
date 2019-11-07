using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentAssertions;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper;
using Rocket.Surgery.Extensions.AutoMapper.Converters;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class LocalDateTests : TypeConverterTest<LocalDateConverter>
    {
        [Theory]
        [MemberData(nameof(GetTestCases))]
        public void AutomatedTests(Type source, Type destination, object sourceValue)
        {
            var method = typeof(IMapper).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .First(x => x.ContainsGenericParameters && x.IsGenericMethodDefinition && x.GetGenericMethodDefinition().GetGenericArguments().Length == 2 && x.GetParameters().Length == 1);
            var result = method.MakeGenericMethod(source, destination).Invoke(_mapper, new[] { sourceValue });

            if (sourceValue == null)
            {
                result.Should().BeNull();
            }
            else
            {
                result.Should().BeOfType(Nullable.GetUnderlyingType(destination) ?? destination).And.NotBeNull();
            }
        }

        protected override void Configure(IMapperConfigurationExpression x)
        {
            x.CreateMap<Foo1, Foo3>().ReverseMap();
        }

        [Fact]
        public void ValidateMapping() => _config.AssertConfigurationIsValid();

        [Fact]
        public void MapsFrom()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = LocalDate.FromDateTime(DateTime.Now)
            };

            var result = mapper.Map<Foo3>(foo).Bar;
            result.Should().Be(foo.Bar.ToDateTimeUnspecified());
        }

        [Fact]
        public void MapsTo()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo3()
            {
                Bar = DateTime.Now
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(LocalDate.FromDateTime(foo.Bar));
        }

        public class Foo1
        {
            public LocalDate Bar { get; set; }
        }

        public class Foo3
        {
            public DateTime Bar { get; set; }
        }
    }
}
