
internal static class HashCode
{
    public static int Compute<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
    {
        unchecked
        {
            var hash = 13;
            hash = (hash * 7) + (arg1?.GetHashCode() ?? 0);
            hash = (hash * 7) + (arg2?.GetHashCode() ?? 0);
            hash = (hash * 7) + (arg3?.GetHashCode() ?? 0);
            return hash;
        }
    }
}