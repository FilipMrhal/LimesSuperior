using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.Random;

public class LimSup
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

    private readonly Vector2[] KArray = new[] { new Vector2((float) _rand.NextDouble(), (float) _rand.NextDouble()) };

    internal void GetFirstColumnOfTheMatrix()
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

    private Complex32 GetSumForIndex(Vector2[] positions) =>
        positions.Aggregate<Vector2, Complex32>(0, (current, position) => current + Complex32.Exp((Complex32) (2 * Math.PI * Complex.ImaginaryOne) * (position.X * KArray[0].X + position.Y * KArray[0].Y)));

    private void GetCoordinatesBasedOnIndexInRho1(int index, ref int x, ref int y)
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

    private void Swap(int[] str, int i, int j) => (str[i], str[j]) = (str[j], str[i]);

    private void Reverse(int[] str, int l, int h)
    {
        while (l < h)
        {
            Swap(str, l, h);
            l++;
            h--;
        }
    }

    private int FindCeil(int[] str, int first, int l, int h)
    {
        var ceilIndex = l;
        for (var i = l + 1; i <= h; i++)
            if (str[i] > first && str[i] < str[ceilIndex])
                ceilIndex = i;

        return ceilIndex;
    }

    private readonly Dictionary<Permutation, bool> _permutations = new();

    public void SortedPermutations(int[] str)
    {
        var stop = new Stopwatch();
        stop.Start();
        // Get size of string
        var size = str.Length;
        long incorrectCount = 0;
        long correctCount = 0;

        //Array.Sort(str);

        var isFinished = false;
        while (!isFinished)
        {
            var newArray = Replace(str);
            if (!_permutations.ContainsKey(newArray))
            {
                _permutations.Add(newArray, false);
                correctCount++;
                Console.WriteLine($"Correct: {correctCount}, Incorrect: {incorrectCount}");
            }
            else
            {
                incorrectCount++;
            }
            // if (Verify(str))
            //     //Console.WriteLine(string.Join(',', str));
            //     correctCount++;
            // else
            //     incorrectCount++;

            int i;
            for (i = size - 2; i >= 0; --i)
                if (str[i] < str[i + 1])
                    break;

            if (i == -1)
                isFinished = true;
            else
            {
                var ceilIndex = FindCeil(str, str[i], i + 1, size - 1);
                Swap(str, i, ceilIndex);
                Reverse(str, i + 1, size - 1);
            }
        }
        stop.Stop();
        Console.WriteLine(stop.Elapsed.TotalMinutes);
        Console.WriteLine(correctCount);
        Console.WriteLine(incorrectCount);
    }

    private bool[,] _verification = new bool[LAMBDA + 3, LAMBDA + 3];
    private bool[,] _verificationRho2 = new bool[LAMBDA, LAMBDA + 3];

    private bool Verify(int[] str)
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

    private Permutation Replace(int[] toReplace) =>
        new(toReplace.Select(x =>
        {
            return x switch
            {
                1 => 1,
                2 => 2,
                5 => 2,
                6 => 2,
                3 => 3,
                7 => 3,
                8 => 3,
                _ => 4
            };
        }).ToArray());

    private int GetLowestAvailableY()
    {
        for (var i = 0; i <= _verification.GetUpperBound(0); i++)
        for (var j = 0; j <= _verification.GetUpperBound(0); j++)
            if (_verification[j, i] == false)
                return i;
        return -1;
    }

    private int GetNextAvailableX()
    {
        for (var i = 0; i <= _verification.GetUpperBound(0); i++)
        for (var j = 0; j <= _verification.GetUpperBound(0); j++)
            if (_verification[j, i] == false)
                return j;
        return -1;
    }

    private bool AddObjectToVerificationArray(int objectId, ref int x, int y)
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

    private bool AddObjectToVerificationArrayRho2(int objectId, ref int x, int y)
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

    private bool IsCorrect(int xFrom, int xTo, int yFrom, int yTo)
    {
        for (var i = xFrom; i < xTo; i++)
        for (var j = yFrom; j < yTo; j++)
            if (_verification[i, j])
                return false;
        return true;
    }

    private void SetValues(int xFrom, int xTo, int yFrom, int yTo)
    {
        for (var i = xFrom; i < xTo; i++)
        for (var j = yFrom; j < yTo; j++)
            _verification[i, j] = true;
    }

    private void CleanUp()
    {
        for (var i = 0; i <= _verification.GetUpperBound(0); i++)
        for (var j = 0; j <= _verification.GetUpperBound(0); j++)
            _verification[i, j] = false;
    }
}

public class Permutation : IEquatable<Permutation>
{
    public readonly int[] _perm;

    public Permutation(int[] perm)
    {
        _perm = perm;
    }

    public bool Equals(Permutation other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return _perm.SequenceEqual(other._perm);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != this.GetType())
            return false;
        return Equals((Permutation) obj);
    }

    public override int GetHashCode() => _perm != null ? _perm.Sum() : 0;
}