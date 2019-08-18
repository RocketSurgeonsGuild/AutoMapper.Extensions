using System;
using AutoMapper;
using FluentAssertions;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class OffsetTests
    {
        private readonly MapperConfiguration _config;

        public OffsetTests()
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
                Bar = Offset.FromHours(11)
            };

            var result = mapper.Map<Foo3>(foo).Bar;
            result.Should().Be(foo.Bar.ToTimeSpan());
        }

        [Fact]
        public void MapsTo()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo3()
            {
                Bar = TimeSpan.FromHours(10)
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Offset.FromTimeSpan(foo.Bar));
        }

        public class Foo1
        {
            public Offset Bar { get; set; }
        }

        public class Foo3
        {
            public TimeSpan Bar { get; set; }
        }
    }
}
