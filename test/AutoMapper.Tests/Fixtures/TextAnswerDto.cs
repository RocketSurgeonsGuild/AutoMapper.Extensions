using Rocket.Surgery.Unions;

namespace Rocket.Surgery.AutoMapper.Tests.Fixtures
{
    [Union(AnswerType.Text)]
    internal sealed class TextAnswerDto : AnswerDto
    {
        public TextAnswerDto() : base(AnswerType.Text) { }
        public string Label { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as TextAnswerDto);
        }

        public override bool Equals(AnswerDto other)
        {
            return other is TextAnswerDto answer &&
                   IsEqual(other) &&
                   Label == answer.Label;
        }
    }
}
