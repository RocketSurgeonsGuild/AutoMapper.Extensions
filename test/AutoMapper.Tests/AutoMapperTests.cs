using System;
using System.Linq;
using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rocket.Surgery.Core.AutoMapper;
using Rocket.Surgery.Core.AutoMapper.Builders;
using Rocket.Surgery.Conventions.Reflection;
using Rocket.Surgery.Conventions.Scanners;
using Rocket.Surgery.Extensions.DependencyInjection;
using Rocket.Surgery.Extensions.Testing;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;
using System.Reflection;
using Rocket.Surgery.AutoMapper.Tests.Fixtures;

namespace Rocket.Surgery.AutoMapper.Tests
{
    public class AutoMapperServicesBuilderTests : AutoTestBase
    {
        public AutoMapperServicesBuilderTests(ITestOutputHelper outputHelper) : base(outputHelper) { }

        [Fact]
        public void MustCallDelegatesOnBuild()
        {
            var configurationDelegate = A.Fake<AutoMapperConfigurationDelegate>();
            var componentConfigurationDelegate = A.Fake<AutoMapperComponentConfigurationDelegate>();

            AutoFake.Provide<IServiceCollection>(new ServiceCollection());
            var servicesBuilder = AutoFake.Resolve<ServicesBuilder>();
            servicesBuilder.Services.AddSingleton<ILoggerFactory>(LoggerFactory);
            var AutoMapperBuilder = servicesBuilder
                .WithAutoMapper()
                .UseStaticRegistration()
                .Configure(configurationDelegate)
                .Configure(componentConfigurationDelegate);

            var serviceProvider = servicesBuilder.Build();

            var mapper = serviceProvider.GetRequiredService<IMapper>();
            mapper.Should().NotBeNull();

            var config = serviceProvider.GetRequiredService<global::AutoMapper.IConfigurationProvider>();
            config.Should().NotBeNull();

            A.CallTo(() => configurationDelegate(A<IMapperConfigurationExpression>._))
                .MustHaveHappened(Repeated.Exactly.Once)
                .Then(A.CallTo(() => componentConfigurationDelegate(serviceProvider, A<IMapperConfigurationExpression>._))
                    .MustHaveHappened(Repeated.Exactly.Once));
        }

        [Fact]
        public void Should_Work_Without_StaticRegistration()
        {
            var configurationDelegate = A.Fake<AutoMapperConfigurationDelegate>();
            var componentConfigurationDelegate = A.Fake<AutoMapperComponentConfigurationDelegate>();

            AutoFake.Provide<IServiceCollection>(new ServiceCollection());
            var servicesBuilder = AutoFake.Resolve<ServicesBuilder>();
            servicesBuilder.Services.AddSingleton<ILoggerFactory>(LoggerFactory);
            var AutoMapperBuilder = servicesBuilder
                .WithAutoMapper()
                .Configure(configurationDelegate)
                .Configure(componentConfigurationDelegate);

            var serviceProvider = servicesBuilder.Build();

            var mapper = serviceProvider.GetRequiredService<IMapper>();
            mapper.Should().NotBeNull();

            var config = serviceProvider.GetRequiredService<global::AutoMapper.IConfigurationProvider>();
            config.Should().NotBeNull();

            A.CallTo(() => configurationDelegate(A<IMapperConfigurationExpression>._))
                .MustHaveHappened(Repeated.Exactly.Once)
                .Then(A.CallTo(() => componentConfigurationDelegate(A<IServiceProvider>._, A<IMapperConfigurationExpression>._))
                    .MustHaveHappened(Repeated.Exactly.Once));
        }

        class Profile1 : Profile { }
        class Profile2 : Profile { }

        [Fact]
        public void Should_Add_Profiles()
        {
            var configurationDelegate = A.Fake<AutoMapperConfigurationDelegate>();
            var componentConfigurationDelegate = A.Fake<AutoMapperComponentConfigurationDelegate>();

            AutoFake.Provide<IServiceCollection>(new ServiceCollection());
            var servicesBuilder = AutoFake.Resolve<ServicesBuilder>();
            servicesBuilder.Services.AddSingleton<ILoggerFactory>(LoggerFactory);
            var AutoMapperBuilder = servicesBuilder
                .WithAutoMapper()
                .WithProfile(typeof(Profile1))
                .WithProfile(typeof(Profile2).GetTypeInfo());

            var serviceProvider = servicesBuilder.Build();

        }
    }

    public class UnionMapperTests : AutoTestBase
    {
        private readonly IMapper _mapper;

        public UnionMapperTests(ITestOutputHelper outputHelper) : base(outputHelper, LogLevel.Debug)
        {
            _mapper = new MapperConfiguration(expression =>
            {
                expression.MapUnions().MapDtoToModel().MapModelToDto();
            }).CreateMapper();
        }

        [Fact]
        public void Should_MapFrom_Dto_To_TextModel()
        {
            var result = _mapper.Map<TextAnswerModel>(
                new TextAnswerDto()
                {
                    Label = "123"
                });

            result.Should().NotBeNull();
            result.Label.Should().Be("123");
        }

        [Fact]
        public void Should_MapFrom_Model_To_TextDto()
        {
            var result = _mapper.Map<TextAnswerDto>(
                new TextAnswerModel()
                {
                    Label = "123"
                });

            result.Should().NotBeNull();
            result.Label.Should().Be("123");
        }



        [Fact]
        public void Should_MapFrom_Dto_To_ValueModel()
        {
            var result = _mapper.Map<ValueAnswerModel>(
                new ValueAnswerDto()
                {
                    Value = "Value2"
                });

            result.Should().NotBeNull();
            result.Value.Should().Be("Value2");
        }

        [Fact]
        public void Should_MapFrom_Model_To_ValueDto()
        {
            var result = _mapper.Map<ValueAnswerDto>(
                new ValueAnswerModel()
                {
                    Value = "Value2"
                });

            result.Should().NotBeNull();
            result.Value.Should().Be("Value2");
        }
    }
}
