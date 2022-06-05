using System.Runtime.InteropServices;

namespace WithTree;

public class TreeNode
{
    public TreeNode(int element,Dictionary<int, int> previousRemaining)
    {
        Element = element;
        Remaining = previousRemaining;
        Remaining[element] -= 1;
    }

    /// <summary>
    /// 1,2,3,4
    /// </summary>
    public int Element { get; set; }
    public List<TreeNode> Children { get; set; } = new();
    public Dictionary<int, int> Remaining;
    public bool Written { get; set; } = false;
}
