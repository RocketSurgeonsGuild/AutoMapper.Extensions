using System;
using AutoMapper;
using FluentAssertions;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class DurationTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;

        public DurationTests()
        {
            _config = new MapperConfiguration(x =>
            {
                x.AddProfile<NodaTimeProfile>();
                x.CreateMap<Foo1, Foo3>().ReverseMap();
                x.CreateMap<Foo1, Foo5>().ReverseMap();
                x.CreateMap<Foo1, Foo7>().ReverseMap();
                x.CreateMap<Foo1, Foo8>().ReverseMap();
                x.CreateMap<Foo1, Foo9>().ReverseMap();
            }
            );
            _mapper = _config.CreateMapper();
        }

        [Fact]
        public void ValidateMapping() => _config.AssertConfigurationIsValid();

        [Fact]
        public void CanConvertDurationToMinutes()
        {
            var foo = new Foo1 { Bar = Duration.FromMinutes(300) };

            var o = _mapper.Map<Foo7>(foo);

            Assert.Equal(18000, o.Bar);
        }

        [Fact]
        public void CanConvertMinutesToDuration()
        {
            var foo = new Foo7 { Bar = 300 };

            var o = _mapper.Map<Foo1>(foo);

            Assert.Equal(Duration.FromMinutes(5), o.Bar);
        }

        [Fact]
        public void MapsFrom_TimeSpan()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = Duration.FromDays(1)
            };

            var result = mapper.Map<Foo3>(foo).Bar;
            result.Should().Be(foo.Bar.ToTimeSpan());
        }

        [Fact]
        public void MapsTo_TimeSpan()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo3()
            {
                Bar = TimeSpan.FromDays(1)
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Duration.FromTimeSpan(foo.Bar));
        }

        [Fact]
        public void MapsFrom_Int64()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = Duration.FromDays(1)
            };

            var result = mapper.Map<Foo5>(foo).Bar;
            result.Should().Be(Convert.ToInt64(foo.Bar.BclCompatibleTicks / NodaConstants.TicksPerMillisecond));
        }

        [Fact]
        public void MapsTo_Int64()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo5()
            {
                Bar = 10000L
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Duration.FromTicks(foo.Bar * NodaConstants.TicksPerMillisecond));
        }

        [Fact]
        public void MapsFrom_Int32()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = Duration.FromDays(1)
            };

            var result = mapper.Map<Foo7>(foo).Bar;
            result.Should().Be(Convert.ToInt32(foo.Bar.BclCompatibleTicks / NodaConstants.TicksPerSecond));
        }

        [Fact]
        public void MapsTo_Int32()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo7()
            {
                Bar = 10000
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Duration.FromTicks(foo.Bar * NodaConstants.TicksPerSecond));
        }

        [Fact]
        public void MapsFrom_Double()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = Duration.FromDays(1)
            };

            var result = mapper.Map<Foo8>(foo).Bar;
            result.Should().Be(Convert.ToInt32(foo.Bar.BclCompatibleTicks / NodaConstants.TicksPerMillisecond));
        }

        [Fact]
        public void MapsTo_Double()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo8()
            {
                Bar = 10000.1256d
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Duration.FromTicks(foo.Bar * NodaConstants.TicksPerMillisecond));
        }

        [Fact]
        public void MapsFrom_Decimal()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo1()
            {
                Bar = Duration.FromDays(1)
            };

            var result = mapper.Map<Foo9>(foo).Bar;
            result.Should().Be(Convert.ToInt32(foo.Bar.BclCompatibleTicks / NodaConstants.TicksPerMillisecond));
        }

        [Fact]
        public void MapsTo_Decimal()
        {
            var mapper = _config.CreateMapper();

            var foo = new Foo9()
            {
                Bar = 10000.125M
            };

            var result = mapper.Map<Foo1>(foo).Bar;
            result.Should().Be(Duration.FromTicks((double)foo.Bar * NodaConstants.TicksPerMillisecond));
        }

        public class Foo1
        {
            public Duration Bar { get; set; }
        }

        public class Foo3
        {
            public TimeSpan Bar { get; set; }
        }

        public class Foo5
        {
            public long Bar { get; set; }
        }

        public class Foo7
        {
            public int Bar { get; set; }
        }

        public class Foo8
        {
            public double Bar { get; set; }
        }

        public class Foo9
        {
            public decimal Bar { get; set; }
        }
    }
}
