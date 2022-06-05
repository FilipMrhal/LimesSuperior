namespace LimesSuperiorInC;

public class ArrayComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[] x, int[] y)
    {
        if(ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(x,null) || ReferenceEquals(y, null))
            return false;

        return x.SequenceEqual(y);
    }

    public int GetHashCode(int[] obj)
    {
        return obj.Select(x => x.GetHashCode()).Sum();
    }
}