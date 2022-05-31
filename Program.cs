using System.Text;
using LimesSuperiorInC;

// for (var size = 16; size <= 16; size++)
// {
//     await using var file = new StreamWriter($"/home/filip/Permutations/{size}.txt", new FileStreamOptions
//     {
//         Access = FileAccess.Write,
//         Mode = FileMode.Append,
//         Options = FileOptions.Asynchronous,
//         BufferSize = 1638400
//     });
//     foreach (var permutation in PermutationsGenerator.SortedPermutations(new []{1,2,2,2,3,3,3,4,4,4,4,4,4,4,4,4}))
//     {
//         Console.WriteLine(string.Join(' ',permutation));
//         await file.WriteLineAsync(string.Join(' ', permutation));
//     }
// }

int[] str =
{
    1, 2, 5, 6, 3, 4, 9, 10, 7, 11, 12, 13, 8, 14, 15, 16
};
// LimSup.StartVerificationQueue();
LimSup.SortedPermutations(str);