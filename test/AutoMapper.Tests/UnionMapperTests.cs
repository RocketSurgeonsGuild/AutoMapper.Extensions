// using System;
// using System.Linq;
// using Autofac.Extras.FakeItEasy;
// using FakeItEasy;
// using AutoMapper;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
// using Rocket.Surgery.Extensions.AutoMapper;
// using Rocket.Surgery.Conventions.Reflection;
// using Rocket.Surgery.Conventions.Scanners;
// using Rocket.Surgery.Extensions.DependencyInjection;
// using Rocket.Surgery.Extensions.Testing;
// using Xunit;
// using Xunit.Abstractions;
// using FluentAssertions;
// using System.Reflection;
// using Rocket.Surgery.AutoMapper.Tests.Fixtures;

// namespace Rocket.Surgery.AutoMapper.Tests
// {
//     public class UnionMapperTests : AutoTestBase
//     {
//         private readonly IMapper _mapper;

//         public UnionMapperTests(ITestOutputHelper outputHelper) : base(outputHelper, LogLevel.Debug)
//         {
//             _mapper = new MapperConfiguration(expression => expression.MapUnions(new[] { typeof(TextAnswerModel).Assembly })).CreateMapper();
//         }

//         [Fact]
//         public void Should_MapFrom_Dto_To_TextModel()
//         {
//             var result = _mapper.Map<TextAnswerModel>(
//                 new TextAnswerDto()
//                 {
//                     Label = "123"
//                 });

//             result.Should().NotBeNull();
//             result.Label.Should().Be("123");
//         }

//         [Fact]
//         public void Should_MapFrom_Model_To_TextDto()
//         {
//             var result = _mapper.Map<TextAnswerDto>(
//                 new TextAnswerModel()
//                 {
//                     Label = "123"
//                 });

//             result.Should().NotBeNull();
//             result.Label.Should().Be("123");
//         }



//         [Fact]
//         public void Should_MapFrom_Dto_To_ValueModel()
//         {
//             var result = _mapper.Map<ValueAnswerModel>(
//                 new ValueAnswerDto()
//                 {
//                     Value = "Value2"
//                 });

//             result.Should().NotBeNull();
//             result.Value.Should().Be("Value2");
//         }

//         [Fact]
//         public void Should_MapFrom_Model_To_ValueDto()
//         {
//             var result = _mapper.Map<ValueAnswerDto>(
//                 new ValueAnswerModel()
//                 {
//                     Value = "Value2"
//                 });

//             result.Should().NotBeNull();
//             result.Value.Should().Be("Value2");
//         }
//     }
// }
