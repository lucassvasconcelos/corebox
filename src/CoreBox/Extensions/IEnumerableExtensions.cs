namespace CoreBox.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T>? sequence)
        => sequence ?? [];
}