using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;

namespace MapperExamples
{
    public static class Config
    {
        public static List<T> CreateObjects<T>(int count)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture.CreateMany<T>(count).ToList();
        }
    }
}