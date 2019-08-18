using System;
using AutoMapper;
using FluentAssertions;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class InstantTests
    {
        private readonly MapperConfiguration _config;

        public InstantTests()
        {
            _config = new MapperConfiguration(x =>
            {
                x.AddProfile<NodaTimeProfile>();

                x.CreateMap<Foo1, Foo3>().ReverseMap();
                x.CreateMap<Foo1, Foo5>().ReverseMap();
            }
            );
        }

        [Fact]
        public void ValidateMapping() => _config.AssertConfigurationIsValid();

        [Fact]
        public void MapsFrom_DateTime()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = Instant.FromDateTimeOffset(DateTimeOffset.Now)
            };

            var result = mapper.Map<Foo3>(foo).Bar;
            result.Should().Be(foo.Bar.ToDateTimeOffset().UtcDateTime);
        }

        [Fact]
        public void MapsTo_DateTime()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo3()
            {
                Bar = DateTime.UtcNow
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Instant.FromDateTimeUtc(foo.Bar));
        }

        [Fact]
        public void MapsFrom_DateTimeOffset()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = Instant.FromDateTimeOffset(DateTimeOffset.Now)
            };

            var result = mapper.Map<Foo5>(foo).Bar;
            result.Should().Be(foo.Bar.ToDateTimeOffset());
        }

        [Fact]
        public void MapsTo_DateTimeOffset()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo5()
            {
                Bar = DateTimeOffset.Now
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Instant.FromDateTimeOffset(foo.Bar));
        }

        public class Foo1
        {
            public Instant Bar { get; set; }
        }

        public class Foo3
        {
            public DateTime Bar { get; set; }
        }

        public class Foo5
        {
            public DateTimeOffset Bar { get; set; }
        }
    }
}
