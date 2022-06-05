using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Xml;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using Microsoft.VisualBasic;
using WithTree;
var roots = new TreeNode[4];

var sw = new Stopwatch();
sw.Start();
for (byte i = 1; i <= 4; i++)
{
    roots[i - 1] = new TreeNode(i, new Dictionary<int, int> { { 1, 1 }, { 2, 3 }, { 3, 3 }, { 4, 9 } });
    roots[i - 1].Children = GetChildren(roots[i - 1].Remaining);
}

var finalPermutations = new List<int[]>();

var permutation = new int[16];
int correctCount = 0;
for (var x = 0; x < roots.Length; x++)
{
    if (x > 0)
        roots[x - 1] = null;
    while (roots[x].Children.Any(c => c.Written == false))
    {
        var node = roots[x];
        var j = 0;
        permutation[j] = node.Element;
        j++;
        while (node.Children.Any(c => c.Written == false))
        {
            node = node.Children.First(c => c.Written == false);
            permutation[j] = node.Element;
            j++;
        }
        node.Written = true;
        if (j == 16 && LimSup.Verify(permutation))//Full permutation
        {
            var newCopy = new int[16];
            Array.Copy(permutation, newCopy, 16);
            finalPermutations.Add(newCopy);
            Console.WriteLine(string.Join(',', permutation));
            correctCount++;
        }
        for (var i = 0; i < permutation.Length; i++)
            permutation[i] = 0;
    }
}
Console.WriteLine(correctCount);

Console.WriteLine();

sw.Stop();
Console.WriteLine(sw.Elapsed.TotalSeconds);

List<TreeNode> GetChildren(IDictionary<int, int> available)
{
    var result = new List<TreeNode>();
    foreach (var number in Enumerable.Range(1, 4).Where(x => available[x] > 0))
    {
        var node = new TreeNode(number, new Dictionary<int, int>(available));
        node.Children = GetChildren(node.Remaining);
        result.Add(node);
    }
    return result;
}

public static class LimSup
{
    private const int LAMBDA = 13;

    private static readonly int[] Rho1 =
    {
        1, 2, 2, 2, 3, 4, 4, 4, 3, 4, 4, 4, 3, 4, 4, 4
    };

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

    private static readonly Vector2[] KArray = new[] { new Vector2((float) _rand.NextDouble(), (float) _rand.NextDouble()) };

    internal static void GetFirstColumnOfTheMatrix()
    {
        Complex32 t11;
        Complex32 t21;
        Complex32 t31;
        Complex32 t41;
        var x = 0;
        var y = 0;
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
            coordinates[i] = new Vector2((float) (coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float) (coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
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
            coordinates[i] = new Vector2((float) (coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float) (coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
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
            coordinates[i] = new Vector2((float) (coordinates[i].X >= 13 ? coordinates[i].X - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].X),
                (float) (coordinates[i].Y >= 13 ? coordinates[i].Y - 13 + (1 + Math.Sqrt(13)) / 2 : coordinates[i].Y));
        t41 = GetSumForIndex(coordinates.ToArray());


    }

    private static Complex32 GetSumForIndex(IEnumerable<Vector2> positions) =>
        positions.Aggregate<Vector2, Complex32>(0, (current, position) => current + Complex32.Exp((Complex32) (2 * Math.PI * Complex.ImaginaryOne) * (position.X * KArray[0].X + position.Y * KArray[0].Y)));

    private static void GetCoordinatesBasedOnIndexInRho1(int index, ref int x, ref int y)
    {
        var i = 0;
        while (i < index)
        {
            AddObjectToVerificationArray(Rho1[i], ref x, y);
            y = GetLowestAvailableY();
            i++;
        }
        x = GetNextAvailableX();
        y = GetLowestAvailableY();
        CleanUp();
    }


    private static bool[,] _verification = new bool[LAMBDA + 3, LAMBDA + 3];
    private static bool[,] _verificationRho2 = new bool[LAMBDA, LAMBDA + 3];

    public static bool Verify(int[] str)
    {
        CleanUp();
        var x = 0;
        var y = 0;
        foreach (var i in str)
        {
            if (!AddObjectToVerificationArray(i, ref x, y))
                return false;
            y = GetLowestAvailableY();
            if (y < 0 && IsCorrect(0, str.GetUpperBound(0), 0, str.GetUpperBound(0)))
                return false;
        }
        return true;
    }

    private static int GetLowestAvailableY()
    {
        for (var i = 0; i <= _verification.GetUpperBound(0); i++)
        for (var j = 0; j <= _verification.GetUpperBound(0); j++)
            if (_verification[j, i] == false)
                return i;
        return -1;
    }

    private static int GetNextAvailableX()
    {
        for (var i = 0; i <= _verification.GetUpperBound(0); i++)
        for (var j = 0; j <= _verification.GetUpperBound(0); j++)
            if (_verification[j, i] == false)
                return j;
        return -1;
    }

    private static bool AddObjectToVerificationArray(int objectId, ref int x, int y)
    {
        switch (objectId)
        {
            //LAMBDA x LAMBDA
            case 1 when x + LAMBDA > _verification.GetUpperBound(0) || y + LAMBDA > _verification.GetUpperBound(1) || IsCorrect(x, x + LAMBDA, y, y + LAMBDA) == false:
                return false;
            case 1:
                SetValues(x, x + LAMBDA, y, y + LAMBDA);
                x = x + LAMBDA <= _verification.GetUpperBound(0) ? x + LAMBDA : 0;
                return true;
            //LAMBDA x 1
            case 3 when x + LAMBDA > _verification.GetUpperBound(0) || y > _verification.GetUpperBound(1) || IsCorrect(x, x + LAMBDA, y, y + 1) == false:
                return false;
            case 3:
                SetValues(x, x + LAMBDA, y, y + 1);
                x = x + LAMBDA <= _verification.GetUpperBound(0) ? x + LAMBDA : 0;
                return true;
            case 2 when x > _verification.GetUpperBound(0) || y + LAMBDA > _verification.GetUpperBound(1) || IsCorrect(x, x + 1, y, y + LAMBDA) == false:
                return false;
            case 2:
                SetValues(x, x + 1, y, y + LAMBDA);
                x = x < _verification.GetUpperBound(0) ? x + 1 : 0;
                return true;
            case 4 when x > _verification.GetUpperBound(0) || y > _verification.GetUpperBound(1) || _verification[x, y]:
                return false;
            case 4:
                _verification[x, y] = true;
                x = x < _verification.GetUpperBound(0) ? x + 1 : 0;
                return true;
            default: return false;
        }
    }

    private static bool AddObjectToVerificationArrayRho2(int objectId, ref int x, int y)
    {
        switch (objectId)
        {
            //LAMBDA x LAMBDA
            case 1 when x + LAMBDA > _verificationRho2.GetUpperBound(0) || y + LAMBDA > _verificationRho2.GetUpperBound(1) || IsCorrect(x, x + LAMBDA, y, y + LAMBDA) == false:
                return false;
            case 1:
                SetValues(x, x + LAMBDA, y, y + LAMBDA);
                x = x + LAMBDA <= _verificationRho2.GetUpperBound(0) ? x + LAMBDA : 0;
                return true;
            //LAMBDA x 1
            case 3 when x + LAMBDA > _verificationRho2.GetUpperBound(0) || y > _verificationRho2.GetUpperBound(1) || IsCorrect(x, x + LAMBDA, y, y + 1) == false:
                return false;
            case 3:
                SetValues(x, x + LAMBDA, y, y + 1);
                x = x + LAMBDA <= _verificationRho2.GetUpperBound(0) ? x + LAMBDA : 0;
                return true;
            case 2 when x > _verificationRho2.GetUpperBound(0) || y + LAMBDA > _verificationRho2.GetUpperBound(1) || IsCorrect(x, x + 1, y, y + LAMBDA) == false:
                return false;
            case 2:
                SetValues(x, x + 1, y, y + LAMBDA);
                x = x < _verificationRho2.GetUpperBound(0) ? x + 1 : 0;
                return true;
            case 4 when x > _verificationRho2.GetUpperBound(0) || y > _verificationRho2.GetUpperBound(1) || _verificationRho2[x, y]:
                return false;
            case 4:
                _verificationRho2[x, y] = true;
                x = x < _verificationRho2.GetUpperBound(0) ? x + 1 : 0;
                return true;
            default: return false;
        }
    }

    private static bool IsCorrect(int xFrom, int xTo, int yFrom, int yTo)
    {
        for (var i = xFrom; i < xTo; i++)
        for (var j = yFrom; j < yTo; j++)
            if (_verification[i, j])
                return false;
        return true;
    }

    private static void SetValues(int xFrom, int xTo, int yFrom, int yTo)
    {
        for (var i = xFrom; i < xTo; i++)
        for (var j = yFrom; j < yTo; j++)
            _verification[i, j] = true;
    }

    private static void CleanUp()
    {
        for (var i = 0; i <= _verification.GetUpperBound(0); i++)
        for (var j = 0; j <= _verification.GetUpperBound(0); j++)
            _verification[i, j] = false;
    }
}