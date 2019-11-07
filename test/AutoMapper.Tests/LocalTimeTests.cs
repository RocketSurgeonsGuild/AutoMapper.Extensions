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
    public class LocalTimeTests : TypeConverterTest<LocalTimeConverter>
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
            x.CreateMap<Foo1, Foo5>().ReverseMap();
        }

        [Fact]
        public void ValidateMapping() => _config.AssertConfigurationIsValid();

        [Fact]
        public void MapsFrom_DateTime()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = LocalTime.FromTicksSinceMidnight(10000)
            };

            var result = mapper.Map<Foo3>(foo).Bar;
            result.Should().Be(new TimeSpan(foo.Bar.TickOfDay));
        }

        [Fact]
        public void MapsTo_DateTime()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo3()
            {
                Bar = TimeSpan.FromMinutes(502)
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(new LocalTime(502 / 60, 502 % 60));
        }

        [Fact]
        public void MapsFrom_DateTimeOffset()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = LocalTime.FromTicksSinceMidnight(10000)
            };

            var result = mapper.Map<Foo5>(foo).Bar;
            result.Should().Be(foo.Bar.On(new LocalDate(1, 1, 1)).ToDateTimeUnspecified());
        }

        [Fact]
        public void MapsTo_DateTimeOffset()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo5()
            {
                Bar = DateTime.Now
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(LocalDateTime.FromDateTime(foo.Bar).TimeOfDay);
        }

        public class Foo1
        {
            public LocalTime Bar { get; set; }
        }

        public class Foo3
        {
            public TimeSpan Bar { get; set; }
        }

        public class Foo5
        {
            public DateTime Bar { get; set; }
        }
    }
}
