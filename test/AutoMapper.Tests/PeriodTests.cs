using AutoMapper;
using FluentAssertions;
using NodaTime;
using NodaTime.Text;
using Rocket.Surgery.Extensions.AutoMapper;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class PeriodTests
    {
        private readonly MapperConfiguration _config;

        public PeriodTests()
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
                Bar = Period.FromMonths(10)
            };

            var result = mapper.Map<Foo3>(foo).Bar;
            result.Should().Be("P10M");
        }

        [Fact]
        public void MapsTo()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo3()
            {
                Bar = "P5M"
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(PeriodPattern.Roundtrip.Parse(foo.Bar).Value);
        }

        public class Foo1
        {
            public Period Bar { get; set; }
        }

        public class Foo3
        {
            public string Bar { get; set; }
        }
    }
}
