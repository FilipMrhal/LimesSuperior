using System.Collections.Concurrent;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.Random;

namespace LimesSuperiorInC;

public class LimSup
{
    private const int LAMBDA = 13;

    private static readonly int[] Rho1 =
    {
        1, 2, 2, 2, 3, 4, 4, 4, 3, 4, 4, 4, 3, 4, 4, 4
    };

    private static readonly int[] Defaults = { 1, 2, 3, 4 };

    private static readonly int[][] Rho2 =
    {
        new[] { 1, 3, 3, 3 }, new[] { 3, 1, 3, 3 }, new[] { 3, 3, 1, 3 }, new[] { 3, 3, 3, 1 }
    };

    private static readonly int[][] Rho3 =
    {
        new[] { 1, 2, 2, 2 }, new[] { 2, 1, 2, 2 }, new[] { 2, 2, 1, 2 }, new[] { 2, 2, 2, 1 }
    };

    private static readonly int[][] Rho4 =
    {
        new[] { 1 }, new[] { 1 }, new[] { 1 }, new[] { 1 }
    };

    private static readonly Random _rand = new Mrg32k3a();

    private static readonly Vector2[] KArray = new[]
        { new Vector2((float)_rand.NextDouble(), (float)_rand.NextDouble()) };

    internal static void GetFirstColumnOfTheMatrix()
    {
        Complex32 t11;
        Complex32 t21;
        Complex32 t31;
        Complex32 t41;
        int x = 0;
        int y = 0;
        for (var i = 0; i < Rho1.Length; i++)
            if (Rho1[i] == 1)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                t11 = GetSumForIndex(new Vector2[] { new(x, y) });
                break;
            }

        x = y = 0;
        var coordinates = new List<Vector2>();
        for (var i = 0; i < Rho1.Length; i++)
        {
            if (Rho1[i] == 2)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                coordinates.Add(new Vector2(x, y));
                x = y = 0;
            }
        }

        for (var i = 0; i < coordinates.Count; i++)
            coordinates[i] = new Vector2(
                (float)(coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float)(coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
        t21 = GetSumForIndex(coordinates.ToArray());

        coordinates = new List<Vector2>();
        for (var i = 0; i < Rho1.Length; i++)
        {
            if (Rho1[i] == 3)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                coordinates.Add(new Vector2(x, y));
                x = y = 0;
            }
        }

        for (var i = 0; i < coordinates.Count; i++)
            coordinates[i] = new Vector2(
                (float)(coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float)(coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
        t31 = GetSumForIndex(coordinates.ToArray());

        coordinates = new List<Vector2>();
        for (var i = 0; i < Rho1.Length; i++)
        {
            if (Rho1[i] == 4)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                coordinates.Add(new Vector2(x, y));
                x = y = 0;
            }
        }

        for (var i = 0; i < coordinates.Count; i++)
            coordinates[i] = new Vector2(
                (float)(coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float)(coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
        t41 = GetSumForIndex(coordinates.ToArray());
    }

    internal static void GetSecondColumnOfTheMatrix()
    {
        Complex32 t12;
        Complex32 t22;
        Complex32 t32;
        Complex32 t42;
        int x = 0;
        int y = 0;
        for (var i = 0; i < Defaults.Length; i++)
            if (Rho1[i] == 1)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                t12 = GetSumForIndex(new Vector2[] { new(x, y) });
                break;
            }

        x = y = 0;
        var coordinates = new List<Vector2>();
        for (var i = 0; i < Rho1.Length; i++)
        {
            if (Rho1[i] == 2)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                coordinates.Add(new Vector2(x, y));
                x = y = 0;
            }
        }

        for (var i = 0; i < coordinates.Count; i++)
            coordinates[i] = new Vector2(
                (float)(coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float)(coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
        t22 = GetSumForIndex(coordinates.ToArray());

        coordinates = new List<Vector2>();
        for (var i = 0; i < Rho1.Length; i++)
        {
            if (Rho1[i] == 3)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                coordinates.Add(new Vector2(x, y));
                x = y = 0;
            }
        }

        for (var i = 0; i < coordinates.Count; i++)
            coordinates[i] = new Vector2(
                (float)(coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float)(coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
        t32 = GetSumForIndex(coordinates.ToArray());

        coordinates = new List<Vector2>();
        for (var i = 0; i < Rho1.Length; i++)
        {
            if (Rho1[i] == 4)
            {
                GetCoordinatesBasedOnIndexInRho1(i, ref x, ref y);
                coordinates.Add(new Vector2(x, y));
                x = y = 0;
            }
        }

        for (var i = 0; i < coordinates.Count; i++)
            coordinates[i] = new Vector2(
                (float)(coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float)(coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
        t42 = GetSumForIndex(coordinates.ToArray());
    }

    private static Complex32 GetSumForIndex(Vector2[] positions) =>
        positions.Aggregate<Vector2, Complex32>(0,
            (current, position) => current + Complex32.Exp((Complex32)(2 * Math.PI * Complex.ImaginaryOne) *
                                                           (position.X * KArray[0].X + position.Y * KArray[0].Y)));

    private static void GetCoordinatesBasedOnIndexInRho1(int index, ref int x, ref int y)
    {
        int i = 0;
        while (i < index)
        {
            AddObjectToVerificationArray(Rho1[i], ref x, y, _verification);
            y = GetLowestAvailableY(_verification);
            i++;
        }

        x = GetNextAvailableX(_verification);
        y = GetLowestAvailableY(_verification);
        CleanUp(_verification);
    }

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

    public static int FindCeil(int[] str, int first, int l, int h)
    {
        int ceilIndex = l;
        for (int i = l + 1; i <= h; i++)
            if (str[i] > first && str[i] < str[ceilIndex])
                ceilIndex = i;

        return ceilIndex;
    }

    private static List<int[]> ToVerify = new();

    private static readonly HashSet<int[]> FinalPermutations = new();

    private static List<Task> verTasks = new();

    // public static void StartVerificationQueue()
    // {
    //     verTasks.AddRange(new[]
    //     {
    //         new Task(VerificationTask),
    //     });
    //     foreach (var task in verTasks) task.Start();
    // }

    // private static void VerificationTask()
    // {
    //     while (ToVerify.TryDequeue(out var result))
    //     {
    //         if (Verify(result, new bool[LAMBDA + 3, LAMBDA + 3]))
    //         {
    //             Replace(result);
    //             if (!FinalPermutations.Any(x => x.SequenceEqual(result)))
    //             {
    //                 FinalPermutations.Add(result);
    //                 Console.WriteLine(string.Join(',', result));
    //             }
    //         }
    //     }
    // }

    public static void SortedPermutations(int[] str)
    {
        // Get size of string
        int size = str.Length;
        long incorrectCount = 0;
        long correctCount = 0;
        long total = 0;

        // Array.Sort(str);
        bool isFinished = false;
        while (!isFinished)
        {
            ToVerify.Add(str);
            incorrectCount++;
            total++;

            int i;
            for (i = size - 2; i >= 0; --i)
                if (str[i] < str[i + 1])
                    break;

            if (i == -1)
                isFinished = true;
            else
            {
                int ceilIndex = FindCeil(str, str[i], i + 1, size - 1);
                Swap(str, i, ceilIndex);
                Reverse(str, i + 1, size - 1);
            }

            if (incorrectCount >= 10000000)
            {
                foreach (var result in ToVerify.AsParallel().Select(Replace).Distinct(new ArrayComparer()))
                {
                    FinalPermutations.Add(result);
                    correctCount++;
                }

                Console.WriteLine(correctCount);
                Console.WriteLine("total " + total);
                incorrectCount = 0;
                ToVerify.Clear();
            }
        }

        Parallel.ForEach(FinalPermutations.Distinct(new ArrayComparer()), permutation =>
        {
            if (Verify(permutation, _verification))
                Console.WriteLine(permutation);
        });


        Console.WriteLine(correctCount);
        Console.WriteLine(incorrectCount);
    }

    private static readonly bool[,] _verification = new bool[LAMBDA + 3, LAMBDA + 3];
    private static readonly bool[,] _verificationRho2 = new bool[LAMBDA, LAMBDA + 3];

    private static int[] Replace(int[] str) =>
        str.Select(x =>
        {
            switch (x)
            {
                case 1:
                    return 1;
                case 2 or 5 or 6:
                    return 2;
                case 3 or 7 or 8:
                    return 3;
                default:
                    return 4;
            }
        }).ToArray();

    internal static bool Verify(int[] str, bool[,] verificationArray)
    {
        CleanUp(verificationArray);
        var x = 0;
        var y = 0;
        int temp;
        foreach (var i in str)
        {
            switch (i)
            {
                case 1:
                    temp = 1;
                    break;
                case 2 or 5 or 6:
                    temp = 2;
                    break;
                case 3 or 7 or 8:
                    temp = 3;
                    break;
                default:
                    temp = 4;
                    break;
            }

            if (!AddObjectToVerificationArray(temp, ref x, y, verificationArray))
                return false;
            y = GetLowestAvailableY(verificationArray);
            if (y < 0 && IsCorrect(0, str.GetUpperBound(0), 0, str.GetUpperBound(0), verificationArray))
                return false;
        }

        return true;
    }

    private static int GetLowestAvailableY(bool[,] verificationArray)
    {
        for (var i = 0; i <= verificationArray.GetUpperBound(0); i++)
        for (int j = 0; j <= verificationArray.GetUpperBound(1); j++)
            if (verificationArray[j, i] == false)
                return i;
        return -1;
    }

    private static int GetNextAvailableX(bool[,] verificationArray)
    {
        for (var i = 0; i <= verificationArray.GetUpperBound(0); i++)
        for (int j = 0; j <= verificationArray.GetUpperBound(1); j++)
            if (verificationArray[j, i] == false)
                return j;
        return -1;
    }

    private static bool AddObjectToVerificationArray(int objectId, ref int x, int y, bool[,] verificationArray)
    {
        switch (objectId)
        {
            //LAMBDA x LAMBDA
            case 1 when x + LAMBDA > verificationArray.GetUpperBound(0) ||
                        y + LAMBDA > verificationArray.GetUpperBound(1) ||
                        IsCorrect(x, x + LAMBDA, y, y + LAMBDA, verificationArray) == false:
                return false;
            case 1:
                SetValues(x, x + LAMBDA, y, y + LAMBDA, verificationArray);
                x = x + LAMBDA <= verificationArray.GetUpperBound(0) ? x + LAMBDA : 0;
                return true;
            //LAMBDA x 1
            case 3 when x + LAMBDA > verificationArray.GetUpperBound(0) || y > verificationArray.GetUpperBound(1) ||
                        IsCorrect(x, x + LAMBDA, y, y + 1, verificationArray) == false:
                return false;
            case 3:
                SetValues(x, x + LAMBDA, y, y + 1, verificationArray);
                x = x + LAMBDA <= verificationArray.GetUpperBound(0) ? x + LAMBDA : 0;
                return true;
            //1 x LAMBDA
            case 2 when x > verificationArray.GetUpperBound(0) || y + LAMBDA > verificationArray.GetUpperBound(1) ||
                        IsCorrect(x, x + 1, y, y + LAMBDA, verificationArray) == false:
                return false;
            case 2:
                SetValues(x, x + 1, y, y + LAMBDA, verificationArray);
                x = x < verificationArray.GetUpperBound(0) ? x + 1 : 0;
                return true;
            case 4 when x > verificationArray.GetUpperBound(0) || y > verificationArray.GetUpperBound(1) ||
                        verificationArray[x, y]:
                return false;
            case 4:
                verificationArray[x, y] = true;
                x = x < verificationArray.GetUpperBound(0) ? x + 1 : 0;
                return true;
            default: return false;
        }
    }

    private static bool IsCorrect(int xFrom, int xTo, int yFrom, int yTo, bool[,] verificationArray)
    {
        for (var i = xFrom; i < xTo; i++)
        for (var j = yFrom; j < yTo; j++)
            if (verificationArray[i, j])
                return false;
        return true;
    }

    private static void SetValues(int xFrom, int xTo, int yFrom, int yTo, bool[,] verificationArray)
    {
        for (var i = xFrom; i < xTo; i++)
        for (var j = yFrom; j < yTo; j++)
            verificationArray[i, j] = true;
    }

    private static void CleanUp(bool[,] verificationArray)
    {
        for (var i = 0; i <= verificationArray.GetUpperBound(0); i++)
        for (var j = 0; j <= verificationArray.GetUpperBound(1); j++)
            verificationArray[i, j] = false;
    }
}