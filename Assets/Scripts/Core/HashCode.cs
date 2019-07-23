
internal static class HashCode
{
    private const int sBaseHash = 13;
    private const int sMultiplier = 7;

    public static int Compute<T1, T2>(T1 arg1, T2 arg2)
    {
        unchecked
        {
            var hash = sBaseHash;
            hash = (hash * sMultiplier) + (arg1?.GetHashCode() ?? 0);
            hash = (hash * sMultiplier) + (arg2?.GetHashCode() ?? 0);
            return hash;
        }
    }

    public static int Compute<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
    {
        unchecked
        {
            var hash = sBaseHash;
            hash = (hash * sMultiplier) + (arg1?.GetHashCode() ?? 0);
            hash = (hash * sMultiplier) + (arg2?.GetHashCode() ?? 0);
            hash = (hash * sMultiplier) + (arg3?.GetHashCode() ?? 0);
            return hash;
        }
    }
}