using System;
using AutoMapper;
using FluentAssertions;
using NodaTime;
using NodaTime.Text;
using Rocket.Surgery.Extensions.AutoMapper;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class OffsetDateTimeTests
    {
        private readonly MapperConfiguration _config;

        public OffsetDateTimeTests()
        {
            _config = new MapperConfiguration(x =>
            {
                x.AddProfile<NodaTimeProfile>();
                x.CreateMap<Foo1, Foo3>().ReverseMap();
            }
            );
        }

        [Fact]
        public void ValidateMapping() => _config.AssertConfigurationIsValid();

        [Fact]
        public void MapsFrom()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = OffsetDateTime.FromDateTimeOffset(DateTimeOffset.Now)
            };

            var result = mapper.Map<Foo3>(foo).Bar;
            result.Should().Be(foo.Bar.ToDateTimeOffset());
        }

        [Fact]
        public void MapsTo()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo3()
            {
                Bar = DateTimeOffset.Now
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(OffsetDateTime.FromDateTimeOffset(foo.Bar));
        }

        public class Foo1
        {
            public OffsetDateTime Bar { get; set; }
        }

        public class Foo3
        {
            public DateTimeOffset Bar { get; set; }
        }
    }
}
