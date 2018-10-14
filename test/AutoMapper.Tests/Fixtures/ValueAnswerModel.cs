using Rocket.Surgery.Unions;

namespace Rocket.Surgery.AutoMapper.Tests.Fixtures
{
    [Union(AnswerType.Value)]
    public class ValueAnswerModel : AnswerModel
    {
        public ValueAnswerModel() : base(AnswerType.Value) { }
        public string Value { get; set; }

        public override bool Equals(AnswerModel other)
        {
            return other is ValueAnswerModel model &&
                   IsEqual(other) &&
                   Value == model.Value;
        }
    }
}
