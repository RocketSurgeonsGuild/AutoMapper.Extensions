using System;
using Rocket.Surgery.Unions;

namespace Rocket.Surgery.AutoMapper.Tests.Fixtures
{
    [Union(AnswerType.Text)]
    public class TextAnswerModel : AnswerModel
    {
        public TextAnswerModel() : base(AnswerType.Text) { }
        public string Label { get; set; }

        public override bool Equals(AnswerModel other)
        {
            return other is TextAnswerModel model &&
                   IsEqual(other) &&
                   Label == model.Label;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Label, Type, Id);
        }
    }
}
