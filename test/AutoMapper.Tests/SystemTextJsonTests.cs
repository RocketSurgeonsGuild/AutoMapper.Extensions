using System;
using System.Text.Json;
using AutoMapper;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Rocket.Surgery.Extensions.AutoMapper.NewtonsoftJson;
using Xunit;

namespace Rocket.Surgery.Extensions.AutoMapper.Tests {
    public class SystemTextJsonTests : TypeConverterTest<JsonElementConverter>
    {
        public SystemTextJsonTests()
        {
        }

        [Fact]
        public void ValidateMapping() => Config.AssertConfigurationIsValid();

        [Theory]
        [InlineData("{}", typeof(JObject))]
        [InlineData("[]", typeof(JArray))]
        [InlineData("null", typeof(JValue))]
        public void ShouldMap_From_Nullable_JsonElementA_To_JToken(string json, Type type)
        {
            var item = new JsonElementA()
            {
                Bar = JsonDocument.Parse(json).RootElement
            };
            var result = Mapper.Map<JTokenA>(item);
            result.Bar.Should().BeOfType(type);
        }
        
        [Fact]
        public void ShouldMap_From_Nullable_JsonElement_To_JToken_Null()
        {
            var item = new JsonElementA()
            {
                Bar = null
            };
            var result = Mapper.Map<JTokenA>(item);
            result.Bar.Should().BeOfType<JValue>();
        }
        
        [Fact]
        public void ShouldMap_From_Nullable_JsonElement_To_JToken_Null_Allow_Nulls()
        {
            var item = new JsonElementA()
            {
                Bar = null
            };
            var result = Mapper.Map<JTokenA>(item);
            result.Bar.Should().BeOfType<JValue>();
        }

        [Theory]
        [ClassData(typeof(ShouldMap_From_JToken_To_Nullable_JsonElement_Data))]
        public void ShouldMap_From_JToken_To_Nullable_JsonElement(string json, JsonValueKind kind)
        {
            var item = new JTokenA()
            {
                Bar = JToken.Parse(json)
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(kind);
        }

        public class ShouldMap_From_JToken_To_Nullable_JsonElement_Data : TheoryData<string, JsonValueKind>
        {
            public ShouldMap_From_JToken_To_Nullable_JsonElement_Data()
            {
                Add("{}", JsonValueKind.Object);
                Add("[]", JsonValueKind.Array);
                Add("null", JsonValueKind.Null);
            }
        }
        
        [Fact]
        public void ShouldMap_From_JToken_To_Nullable_JsonElement_Null()
        {
            var item = new JTokenA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(JsonValueKind.Null);
        }
        
        [Fact]
        public void ShouldMap_From_JToken_To_Nullable_JsonElement_Null_Allow_Nulls()
        {
            var item = new JTokenA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(JsonValueKind.Null);
        }

        [Theory]
        [InlineData("{}", typeof(JObject))]
        [InlineData("[]", typeof(JArray))]
        [InlineData("null", typeof(JValue))]
        public void ShouldMap_From_JsonElement_To_JToken(string json, Type type)
        {
            var item = new JsonElementB()
            {
                Bar = JsonDocument.Parse(json).RootElement
            };
            var result = Mapper.Map<JTokenA>(item);
            result.Bar.Should().BeOfType(type);
        }
        
        [Fact]
        public void ShouldMap_From_JsonElement_To_JToken_Null()
        {
            var item = new JsonElementB()
            {
                Bar = default
            };
            var result = Mapper.Map<JTokenA>(item);
            result.Bar.Should().BeOfType<JValue>();
        }
        
        [Fact]
        public void ShouldMap_From_JsonElement_To_JToken_Null_Allow_Nulls()
        {
            var item = new JsonElementB()
            {
                Bar = default
            };
            var result = Mapper.Map<JTokenA>(item);
            result.Bar.Should().BeOfType<JValue>();
        }

        [Theory]
        [ClassData(typeof(ShouldMap_From_JToken_To_JsonElement_Data))]
        public void ShouldMap_From_JToken_To_JsonElement(string json, JsonValueKind kind)
        {
            var item = new JTokenA()
            {
                Bar = JToken.Parse(json)
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(kind);
        }

        public class ShouldMap_From_JToken_To_JsonElement_Data : TheoryData<string, JsonValueKind>
        {
            public ShouldMap_From_JToken_To_JsonElement_Data()
            {
                Add("{}", JsonValueKind.Object);
                Add("[]", JsonValueKind.Array);
                Add("null", JsonValueKind.Null);
            }
        }
        
        [Fact]
        public void ShouldMap_From_JToken_To_JsonElement_Null()
        {
            var item = new JTokenA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(JsonValueKind.Null);
        }
        
        [Fact]
        public void ShouldMap_From_JToken_To_JsonElement_Null_Allow_Nulls()
        {
            var item = new JTokenA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(JsonValueKind.Null);
        }


        [Theory]
        [InlineData("[]", typeof(JArray))]
        public void ShouldMap_From_Nullable_JsonElementA_To_JArray(string json, Type type)
        {
            var item = new JsonElementA()
            {
                Bar = JsonDocument.Parse(json).RootElement
            };
            var result = Mapper.Map<JArrayA>(item);
            result.Bar.Should().BeOfType(type);
        }
        
        [Fact]
        public void ShouldMap_From_Nullable_JsonElement_To_JArray_Null()
        {
            var item = new JsonElementA()
            {
                Bar = null
            };
            var result = Mapper.Map<JArrayA>(item);
            result.Bar.Should().BeOfType<JArray>();
        }
        
        [Fact]
        public void ShouldMap_From_Nullable_JsonElement_To_JArray_Null_Allow_Nulls()
        {
            var item = new JsonElementA()
            {
                Bar = null
            };
            var result = Mapper.Map<JArrayA>(item);
            result.Bar.Should().BeOfType<JArray>();
        }

        [Theory]
        [ClassData(typeof(ShouldMap_From_JArray_To_Nullable_JsonElement_Data))]
        public void ShouldMap_From_JArray_To_Nullable_JsonElement(string json, JsonValueKind kind)
        {
            var item = new JArrayA()
            {
                Bar = JArray.Parse(json)
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(kind);
        }

        public class ShouldMap_From_JArray_To_Nullable_JsonElement_Data : TheoryData<string, JsonValueKind>
        {
            public ShouldMap_From_JArray_To_Nullable_JsonElement_Data()
            {
                Add("[]", JsonValueKind.Array);
            }
        }
        
        [Fact]
        public void ShouldMap_From_JArray_To_Nullable_JsonElement_Null()
        {
            var item = new JArrayA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(JsonValueKind.Null);
        }
        
        [Fact]
        public void ShouldMap_From_JArray_To_Nullable_JsonElement_Null_Allow_Nulls()
        {
            var item = new JArrayA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(JsonValueKind.Null);
        }

        [Theory]
        [InlineData("[]", typeof(JArray))]
        public void ShouldMap_From_JsonElement_To_JArray(string json, Type type)
        {
            var item = new JsonElementB()
            {
                Bar = JsonDocument.Parse(json).RootElement
            };
            var result = Mapper.Map<JArrayA>(item);
            result.Bar.Should().BeOfType(type);
        }
        
        [Fact]
        public void ShouldMap_From_JsonElement_To_JArray_Null()
        {
            var item = new JsonElementB()
            {
                Bar = default
            };
            var result = Mapper.Map<JArrayA>(item);
            result.Bar.Should().BeOfType<JArray>();
        }
        
        [Fact]
        public void ShouldMap_From_JsonElement_To_JArray_Null_Allow_Nulls()
        {
            var item = new JsonElementB()
            {
                Bar = default
            };
            var result = Mapper.Map<JArrayA>(item);
            result.Bar.Should().BeOfType<JArray>();
        }

        [Theory]
        [ClassData(typeof(ShouldMap_From_JArray_To_JsonElement_Data))]
        public void ShouldMap_From_JArray_To_JsonElement(string json, JsonValueKind kind)
        {
            var item = new JArrayA()
            {
                Bar = JArray.Parse(json)
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(kind);
        }

        public class ShouldMap_From_JArray_To_JsonElement_Data : TheoryData<string, JsonValueKind>
        {
            public ShouldMap_From_JArray_To_JsonElement_Data()
            {
                Add("[]", JsonValueKind.Array);
            }
        }
        
        [Fact]
        public void ShouldMap_From_JArray_To_JsonElement_Null()
        {
            var item = new JArrayA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(JsonValueKind.Null);
        }
        
        [Fact]
        public void ShouldMap_From_JArray_To_JsonElement_Null_Allow_Nulls()
        {
            var item = new JArrayA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(JsonValueKind.Null);
        }


        [Theory]
        [InlineData("{}", typeof(JObject))]
        public void ShouldMap_From_Nullable_JsonElementA_To_JObject(string json, Type type)
        {
            var item = new JsonElementA()
            {
                Bar = JsonDocument.Parse(json).RootElement
            };
            var result = Mapper.Map<JObjectA>(item);
            result.Bar.Should().BeOfType(type);
        }
        
        [Fact]
        public void ShouldMap_From_Nullable_JsonElement_To_JObject_Null()
        {
            var item = new JsonElementA()
            {
                Bar = null
            };
            var result = Mapper.Map<JObjectA>(item);
            result.Bar.Should().BeOfType<JObject>();
        }
        
        [Fact]
        public void ShouldMap_From_Nullable_JsonElement_To_JObject_Null_Allow_Nulls()
        {
            var item = new JsonElementA()
            {
                Bar = null
            };
            var result = Mapper.Map<JObjectA>(item);
            result.Bar.Should().BeOfType<JObject>();
        }

        [Theory]
        [ClassData(typeof(ShouldMap_From_JObject_To_Nullable_JsonElement_Data))]
        public void ShouldMap_From_JObject_To_Nullable_JsonElement(string json, JsonValueKind kind)
        {
            var item = new JObjectA()
            {
                Bar = JObject.Parse(json)
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(kind);
        }

        public class ShouldMap_From_JObject_To_Nullable_JsonElement_Data : TheoryData<string, JsonValueKind>
        {
            public ShouldMap_From_JObject_To_Nullable_JsonElement_Data()
            {
                Add("{}", JsonValueKind.Object);
            }
        }
        
        [Fact]
        public void ShouldMap_From_JObject_To_Nullable_JsonElement_Null()
        {
            var item = new JObjectA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(JsonValueKind.Null);
        }
        
        [Fact]
        public void ShouldMap_From_JObject_To_Nullable_JsonElement_Null_Allow_Nulls()
        {
            var item = new JObjectA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementA>(item);
            result.Bar.Should().NotBeNull();
            result.Bar.Value.ValueKind.Should().Be(JsonValueKind.Null);
        }

        [Theory]
        [InlineData("{}", typeof(JObject))]
        public void ShouldMap_From_JsonElement_To_JObject(string json, Type type)
        {
            var item = new JsonElementB()
            {
                Bar = JsonDocument.Parse(json).RootElement
            };
            var result = Mapper.Map<JObjectA>(item);
            result.Bar.Should().BeOfType(type);
        }
        
        [Fact]
        public void ShouldMap_From_JsonElement_To_JObject_Null()
        {
            var item = new JsonElementB()
            {
                Bar = default
            };
            var result = Mapper.Map<JObjectA>(item);
            result.Bar.Should().BeOfType<JObject>();
        }
        
        [Fact]
        public void ShouldMap_From_JsonElement_To_JObject_Null_Allow_Nulls()
        {
            var item = new JsonElementB()
            {
                Bar = default
            };
            var result = Mapper.Map<JObjectA>(item);
            result.Bar.Should().BeOfType<JObject>();
        }

        [Theory]
        [ClassData(typeof(ShouldMap_From_JObject_To_JsonElement_Data))]
        public void ShouldMap_From_JObject_To_JsonElement(string json, JsonValueKind kind)
        {
            var item = new JObjectA()
            {
                Bar = JObject.Parse(json)
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(kind);
        }

        public class ShouldMap_From_JObject_To_JsonElement_Data : TheoryData<string, JsonValueKind>
        {
            public ShouldMap_From_JObject_To_JsonElement_Data()
            {
                Add("{}", JsonValueKind.Object);
            }
        }
        
        [Fact]
        public void ShouldMap_From_JObject_To_JsonElement_Null()
        {
            var item = new JObjectA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(JsonValueKind.Null);
        }
        
        [Fact]
        public void ShouldMap_From_JObject_To_JsonElement_Null_Allow_Nulls()
        {
            var item = new JObjectA()
            {
                Bar = null
            };
            var result = Mapper.Map<JsonElementB>(item);
            result.Bar.ValueKind.Should().Be(JsonValueKind.Null);
        }

        protected override void Configure(IMapperConfigurationExpression expression)
        {
            expression.AddProfile(new SystemJsonTextProfile());
            expression.AddProfile(new NewtonsoftJsonProfile());
            expression.CreateMap<JsonElementA, JTokenA>();
            expression.CreateMap<JTokenA, JsonElementA>();
            expression.CreateMap<JsonElementB, JTokenA>().ReverseMap();
            expression.CreateMap<JsonElementA, JObjectA>().ReverseMap();
            expression.CreateMap<JsonElementB, JObjectA>().ReverseMap();
            expression.CreateMap<JsonElementA, JArrayA>().ReverseMap();
            expression.CreateMap<JsonElementB, JArrayA>().ReverseMap();
        }

        private class JsonElementA
        {
            public JsonElement? Bar { get; set; }
        }

        private class JTokenA
        {
            public JToken? Bar { get; set; }
        }

        private class JsonElementB
        {
            public JsonElement Bar { get; set; }
        }

        private class JObjectA
        {
            public JObject? Bar { get; set; }
        }

        private class JArrayA
        {
            public JArray? Bar { get; set; }
        }
    }
}