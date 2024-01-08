namespace Libs.Extensions;

// ReSharper disable once InconsistentNaming
public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (T item in collection) action(item);
    }
}