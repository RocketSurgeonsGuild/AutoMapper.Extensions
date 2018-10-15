using System;
using AutoMapper;
using NodaTime;
using Rocket.Surgery.Extensions.AutoMapper;
using Xunit;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class InstantTests
    {
        [Fact]
        public void ValidateMapping()
        {
            var config = new MapperConfiguration(x =>
                {
                    x.AddProfile<NodaTimeProfile>();

                    x.CreateMap<Foo1, Foo3>().ReverseMap();
                    x.CreateMap<Foo2, Foo4>().ReverseMap();
                    x.CreateMap<Foo1, Foo5>().ReverseMap();
                    x.CreateMap<Foo2, Foo6>().ReverseMap();
                }
            );

            config.AssertConfigurationIsValid();
        }

        public class Foo1
        {
            public Instant Bar { get; set; }
        }

        public class Foo2
        {
            public Instant? Bar { get; set; }
        }

        public class Foo3
        {
            public DateTime Bar { get; set; }
        }

        public class Foo4
        {
            public DateTime? Bar { get; set; }
        }

        public class Foo5
        {
            public DateTimeOffset Bar { get; set; }
        }

        public class Foo6
        {
            public DateTimeOffset? Bar { get; set; }
        }
    }
}
