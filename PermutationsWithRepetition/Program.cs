// C# program to print all permutations
// with repetition of characters
using System;
var perm = new int[]{1,2,2,2,3,4,4,4,3,4,4,4,3,4,4,4};
Console.Write("All permutations with " +
              "repetition of {0} are: \n", string.Join(',',perm));
GFG.allLexicographic(perm);


public static class GFG
{
    static void allLexicographicRecur(int[] str, int[] data,
        int last, int index)
    {
        int length = str.Length;
        for (int i = 0; i < length; i++)
        {
            data[index] = str[i];

            if (index == last)
            {
                if (data.Sum() == 52)
                    Console.WriteLine(String.Join(',', data));
            }
            else
                allLexicographicRecur(str, data, last, index + 1);
        }
    }

    public static void allLexicographic(int[] perm)
    {
        int length = perm.Length;

        int[] data = new int[length + 1];
        int[] temp = perm;

        // Array.Sort(temp);
        perm = temp;

        allLexicographicRecur(perm, data, length - 1, 0);
    }
}