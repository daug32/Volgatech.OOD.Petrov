namespace Libs.Extensions;

public static class HashSetExtensions
{
    public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> itemsToAdd)
    {
        foreach (T item in itemsToAdd) set.Add(item);
    }
}