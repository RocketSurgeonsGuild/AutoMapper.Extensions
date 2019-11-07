namespace Rocket.Surgery.AutoMapper.Tests
{
    public class Foo
    {
        public static Foo<T> Create<T>(T value)
        {
            return new Foo<T>() { Value = value };
        }
    }
    public class Foo<T>
    {
        public T Value { get; set; }
    }
}
