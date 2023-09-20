namespace Extensions;

public static class MyExtensions
{
    public static int WordCount(this string str)
    {
        return str.Split(new char[] { ' ', '.', '?' },
                         StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
    {
        if (sequence == null)
        {
            yield break;
        }

        var list = sequence.ToList();

        if (!list.Any())
        {
            yield return Enumerable.Empty<T>();
        }
        else
        {
            var startIndex = 0;

            foreach (var startElement in list)
            {
                var index = startIndex;
                var remainder = list.Where((e, i) => i != index);

                foreach (var permutationOfRemainder in remainder.Permute())
                {
                    yield return startElement.Concat(permutationOfRemainder);
                }

                startIndex++;
            }
        }
    }

    public static IEnumerable<T> Concat<T>(this T item, IEnumerable<T> items)
    {
        yield return item;

        if (items != null)
        {
            foreach (var otherItem in items)
            {
                yield return otherItem;
            }
        }
    }
}
