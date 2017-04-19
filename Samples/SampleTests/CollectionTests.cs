using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace SampleTests
{
    using DevTeam.TestFramework;

    [Test.Types(typeof(List<string>))]
    [Test.Types(typeof(HashSet<string>))]
    public class CollectionTests<T>
        where T : ICollection<string>, new()
    {
        [Test.Args.Source(typeof(StringsSource))]
        public void AddTest(string[] values)
        {
            // Given
            var collection = CreateInstance();

            // When
            foreach (var value in values)
            {
                collection.Add(value);
            }

            // Then
            //values.All(value => collection.Contains(value)).ShouldBeTrue();
        }

        private static T CreateInstance()
        {
            return new T();
        }
    }

    internal class StringsSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { new[] { "abc" } };
            yield return new object[] { new[] { "abc", "xyz" } };
            yield return new object[] { new string[] { } };
        }
    }
}
