namespace Evico.Api.Extensions;

public static class IComparableIsBetweenExtension
{
    public static bool IsBetween<T>(this IComparable input, IComparable from, IComparable to) where T : IComparable
        => from.CompareTo(input) <= 0 && input.CompareTo(to) <= 0;
}