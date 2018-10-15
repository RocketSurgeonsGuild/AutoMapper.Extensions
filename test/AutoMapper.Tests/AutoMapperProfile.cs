using AutoMapper;
using Rocket.Surgery.Extensions.Testing;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using Rocket.Surgery.Extensions.AutoMapper;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public static class AutoMapperProfile
    {
        class ParentModel
        {
            public int Integer { get; set; }
            public int? NullableInteger { get; set; }
            public string String { get; set; }
            public decimal Decimal { get; set; }
            public decimal? NullableDecimal { get; set; }
            public ChildModel Child { get; set; }
        }

        class ParentDto
        {
            public int Integer { get; set; }
            public int Version { get; set; }
            public int? NullableInteger { get; set; }
            public string String { get; set; }
            public decimal Decimal { get; set; }
            public decimal? NullableDecimal { get; set; }
            public ChildDto Child { get; set; }
        }

        class ChildModel
        {
            public int Integer { get; set; }
            public int? NullableInteger { get; set; }
            public string String { get; set; }
            public decimal Decimal { get; set; }
            public decimal? NullableDecimal { get; set; }
        }

        class ChildDto
        {
            public int Integer { get; set; }
            public int Version { get; set; }
            public int? NullableInteger { get; set; }
            public string String { get; set; }
            public decimal Decimal { get; set; }
            public decimal? NullableDecimal { get; set; }
        }

        public class OnlyDefinedPropertiesTests : AutoTestBase
        {
            public OnlyDefinedPropertiesTests(ITestOutputHelper outputHelper) : base(outputHelper) { }

            private class MyProfile : Profile
            {
                protected MyProfile()
                {
                    this.OnlyDefinedProperties();
                }
            }

            [Fact]
            public void ConfigurationIsValid()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ChildModel, ChildDto>()
                        .ForMember(x => x.Version, x => x.Ignore());
                    cfg.CreateMap<ParentModel, ParentDto>()
                        .ForMember(x => x.Version, x => x.Ignore());
                    cfg.OnlyDefinedProperties();
                }).CreateMapper();

                mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }

            [Fact]
            public void ShouldMaintain_AllowNullDestinationValues()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AllowNullDestinationValues = false;
                    cfg.OnlyDefinedProperties();
                    cfg.AllowNullDestinationValues.Should().BeFalse();
                }).CreateMapper();
            }

            [Fact]
            public void ShouldMaintain_AllowNullCollections()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AllowNullCollections = false;
                    cfg.OnlyDefinedProperties();
                    cfg.AllowNullCollections.Should().BeFalse();
                }).CreateMapper();
            }

            [Fact]
            public void MapOnlyPropertiesThatWereSetOnTheLeftHandSide()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.OnlyDefinedProperties();
                    cfg.CreateMap<ChildModel, ChildDto>();
                    cfg.CreateMap<ParentModel, ParentDto>();
                }).CreateMapper();

                var destination = new ParentDto()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123"
                };

                mapper.Map(
                    new ParentModel()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123
                    },
                    destination
                );

                destination.Integer.Should().Be(1337);
                destination.NullableInteger.Should().Be(123);
                destination.Decimal.Should().Be(2.2M);
                destination.NullableDecimal.Should().Be(13.37M);
                destination.String.Should().Be("123");
                destination.Child.Should().BeNull();
            }

            [Fact]
            public void MapOnlyPropertiesThatWereSetOnTheLeftHandSide_WithChildren()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.OnlyDefinedProperties();
                    cfg.CreateMap<ChildModel, ChildDto>();
                    cfg.CreateMap<ParentModel, ParentDto>();
                }).CreateMapper();

                var destination = new ParentDto()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123",
                    Child = new ChildDto()
                    {
                        Integer = 1337,
                        NullableInteger = 1337,
                        Decimal = 13.37M,
                        NullableDecimal = 13.37M,
                        String = "123",
                    }
                };

                mapper.Map(
                    new ParentModel()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123,
                        Child = new ChildModel()
                        {
                            NullableDecimal = 2.2M,
                            Integer = 123,
                        }
                    },
                    destination
                );

                destination.Integer.Should().Be(1337);
                destination.NullableInteger.Should().Be(123);
                destination.Decimal.Should().Be(2.2M);
                destination.NullableDecimal.Should().Be(13.37M);
                destination.String.Should().Be("123");

                destination.Child.Integer.Should().Be(123);
                destination.Child.NullableInteger.Should().Be(1337);
                destination.Child.Decimal.Should().Be(13.37M);
                destination.Child.NullableDecimal.Should().Be(2.2M);
                destination.Child.String.Should().Be("123");
            }
        }

        public class MapWithPostfixTests : AutoTestBase
        {
            public MapWithPostfixTests(ITestOutputHelper outputHelper) : base(outputHelper) { }

            private class DtoToModelProfile : Profile
            {
                public DtoToModelProfile()
                {
                    this.MapDtoToModel();
                }
            }

            private class ModelToDtoProfile : Profile
            {
                public ModelToDtoProfile()
                {
                    this.MapModelToDto();
                }
            }

            [Fact]
            public void Maps_ModelToDto()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ModelToDtoProfile>();
                }).CreateMapper();

                var destination = new ParentDto()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123"
                };

                mapper.Map(
                    new ParentModel()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123
                    },
                    destination
                );

                destination.Integer.Should().Be(1337);
                destination.NullableInteger.Should().Be(123);
                destination.Decimal.Should().Be(2.2M);
                destination.NullableDecimal.Should().Be(13.37M);
                destination.String.Should().Be("123");
                destination.Child.Should().BeNull();
            }

            [Fact]
            public void Maps_ModelToDto_WithChildren()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ModelToDtoProfile>();
                }).CreateMapper();

                var destination = new ParentDto()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123",
                    Child = new ChildDto()
                    {
                        Integer = 1337,
                        NullableInteger = 1337,
                        Decimal = 13.37M,
                        NullableDecimal = 13.37M,
                        String = "123",
                    }
                };

                mapper.Map(
                    new ParentModel()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123,
                        Child = new ChildModel()
                        {
                            NullableDecimal = 2.2M,
                            Integer = 123,
                        }
                    },
                    destination
                );

                destination.Integer.Should().Be(1337);
                destination.NullableInteger.Should().Be(123);
                destination.Decimal.Should().Be(2.2M);
                destination.NullableDecimal.Should().Be(13.37M);
                destination.String.Should().Be("123");

                destination.Child.Integer.Should().Be(123);
                destination.Child.NullableInteger.Should().Be(1337);
                destination.Child.Decimal.Should().Be(13.37M);
                destination.Child.NullableDecimal.Should().Be(2.2M);
                destination.Child.String.Should().Be("123");
            }

            [Fact]
            public void Maps_DtoToModel()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<DtoToModelProfile>();
                }).CreateMapper();

                var destination = new ParentModel()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123"
                };

                mapper.Map(
                    new ParentDto()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123
                    },
                    destination
                );

                destination.Integer.Should().Be(0);
                destination.NullableInteger.Should().Be(123);
                destination.Decimal.Should().Be(2.2M);
                destination.NullableDecimal.Should().BeNull();
                destination.String.Should().Be("");
                destination.Child.Should().NotBeNull();
            }

            [Fact]
            public void Maps_DtoToModel_WithChildren()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ModelToDtoProfile>();
                    cfg.AddProfile<DtoToModelProfile>();
                }).CreateMapper();

                var destination = new ParentDto()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123",
                    Child = new ChildDto()
                    {
                        Integer = 1337,
                        NullableInteger = 1337,
                        Decimal = 13.37M,
                        NullableDecimal = 13.37M,
                        String = "123",
                    }
                };

                mapper.Map(
                    new ParentModel()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123,
                        Child = new ChildModel()
                        {
                            NullableDecimal = 2.2M,
                            Integer = 123,
                        }
                    },
                    destination
                );

                mapper.Map(
                    new ParentDto()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123,
                        Child = new ChildDto()
                        {
                            NullableDecimal = 2.2M,
                            Integer = 123,
                        }
                    },
                    destination
                );

                destination.Integer.Should().Be(1337);
                destination.NullableInteger.Should().Be(123);
                destination.Decimal.Should().Be(2.2M);
                destination.NullableDecimal.Should().Be(13.37M);
                destination.String.Should().Be("123");

                destination.Child.Integer.Should().Be(123);
                destination.Child.NullableInteger.Should().Be(1337);
                destination.Child.Decimal.Should().Be(13.37M);
                destination.Child.NullableDecimal.Should().Be(2.2M);
                destination.Child.String.Should().Be("123");
            }

            [Fact]
            public void Maps_Both_WithChildren()
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<DtoToModelProfile>();
                    cfg.AddProfile<ModelToDtoProfile>();
                }).CreateMapper();

                var destinationDto = new ParentDto()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123",
                    Child = new ChildDto()
                    {
                        Integer = 1337,
                        NullableInteger = 1337,
                        Decimal = 13.37M,
                        NullableDecimal = 13.37M,
                        String = "123",
                    }
                };

                mapper.Map(
                    new ParentModel()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123,
                        Child = new ChildModel()
                        {
                            NullableDecimal = 2.2M,
                            Integer = 123,
                        }
                    },
                    destinationDto
                );

                var destinationModel = new ParentModel()
                {
                    Integer = 1337,
                    NullableInteger = 1337,
                    Decimal = 13.37M,
                    NullableDecimal = 13.37M,
                    String = "123",
                    Child = new ChildModel()
                    {
                        Integer = 1337,
                        NullableInteger = 1337,
                        Decimal = 13.37M,
                        NullableDecimal = 13.37M,
                        String = "123",
                    }
                };

                mapper.Map(
                    new ParentDto()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123,
                        Child = new ChildDto()
                        {
                            NullableDecimal = 2.2M,
                            Integer = 123,
                        }
                    },
                    destinationModel
                );

                mapper.Map(
                    new ParentModel()
                    {
                        Decimal = 2.2M,
                        NullableInteger = 123,
                        Child = new ChildModel()
                        {
                            NullableDecimal = 2.2M,
                            Integer = 123,
                        }
                    },
                    destinationModel
                );

                destinationDto.Integer.Should().Be(1337);
                destinationDto.NullableInteger.Should().Be(123);
                destinationDto.Decimal.Should().Be(2.2M);
                destinationDto.NullableDecimal.Should().Be(13.37M);
                destinationDto.String.Should().Be("123");

                destinationDto.Child.Integer.Should().Be(123);
                destinationDto.Child.NullableInteger.Should().Be(1337);
                destinationDto.Child.Decimal.Should().Be(13.37M);
                destinationDto.Child.NullableDecimal.Should().Be(2.2M);
                destinationDto.Child.String.Should().Be("123");

                destinationModel.Integer.Should().Be(0);
                destinationModel.NullableInteger.Should().Be(123);
                destinationModel.Decimal.Should().Be(2.2M);
                destinationModel.NullableDecimal.Should().BeNull();
                destinationModel.String.Should().Be("");

                destinationModel.Child.Integer.Should().Be(123);
                destinationModel.Child.NullableInteger.Should().BeNull();
                destinationModel.Child.Decimal.Should().Be(0M);
                destinationModel.Child.NullableDecimal.Should().Be(2.2M);
                destinationModel.Child.String.Should().Be("");
            }
        }
    }
}
