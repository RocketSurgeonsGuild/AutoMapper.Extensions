using System;
using Rocket.Surgery.Unions;

namespace Rocket.Surgery.AutoMapper.Tests.Fixtures
{
    [Union(AnswerType.Value)]
    internal sealed class ValueAnswerDto : AnswerDto
    {
        public ValueAnswerDto() : base(AnswerType.Value) { }
        public string Value { get; set; }

        public override bool Equals(AnswerDto other)
        {
            return other is ValueAnswerDto answer &&
                   IsEqual(other) &&
                   Value == answer.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Type, Id);
        }
    }
}
