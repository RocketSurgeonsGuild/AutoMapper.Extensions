using System;
using AutoMapper;
using FluentAssertions;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class LocalDateTimeTests
    {
        private readonly MapperConfiguration _config;

        public LocalDateTimeTests()
        {
            _config = new MapperConfiguration(x =>
            {
                x.AddProfile<NodaTimeProfile>();
                x.CreateMap<Foo1, Foo3>().ReverseMap();
            }
            );
        }

        [Fact]
        public void ValidateMapping()
        {
            _config.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapsFrom()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = LocalDateTime.FromDateTime(DateTime.Now)
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
            result.Should().Be(LocalDateTime.FromDateTime(foo.Bar));
        }

        public class Foo1
        {
            public LocalDateTime Bar { get; set; }
        }

        public class Foo3
        {
            public DateTime Bar { get; set; }
        }
    }
}
