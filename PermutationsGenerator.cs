namespace LimesSuperiorInC;

public static class PermutationsGenerator
{
    private static void Swap(int[] str, int i, int j) => (str[i], str[j]) = (str[j], str[i]);

    private static void Reverse(int[] str, int l, int h)
    {
        while (l < h)
        {
            Swap(str, l, h);
            l++;
            h--;
        }
    }

    private static int FindCeil(int[] str, int first, int l, int h)
    {
        int ceilIndex = l;
        for (int i = l + 1; i <= h; i++)
            if (str[i] > first && str[i] < str[ceilIndex])
                ceilIndex = i;

        return ceilIndex;
    }

    public static IEnumerable<int[]> SortedPermutations(int[] str)
    {
        // Get size of string
        int size = str.Length;
        long finalCount = 1;

        Array.Sort(str);
        yield return str;

        while (true)
        {
            int i;
            for (i = size - 2; i >= 0; --i)
                if (str[i] < str[i + 1])
                    break;
            if (i == -1)
            {
                Console.WriteLine(finalCount);
                yield break;
            }

            int ceilIndex = FindCeil(str, str[i], i + 1, size - 1);
            Swap(str, i, ceilIndex);
            Reverse(str, i + 1, size - 1);
            yield return str;
            finalCount++;
        }
    }
}