using System.Collections;

namespace CreaMT.WebApi.Test.InlineData;
public class CultureInlineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "pt" };
        yield return new object[] { "en" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
