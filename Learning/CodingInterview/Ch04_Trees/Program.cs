using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var tree = GetBinarySearchTree();
        var sb = new StringBuilder();

        Node.TraverseInOrder(tree, PrintVisitor);
        NewLine();
        Node.TraversePreOrder(tree, PrintVisitor);
        NewLine();
        int height = Node.GetHeight(tree);
        sb.Append("Tree level: ").Append(height);
        NewLine();
        sb.Append(PrintTree(tree));
        NewLine();
        System.Console.WriteLine(sb.ToString());

        void PrintVisitor(Node node) => sb.Append(node.Value).Append(",");
        void NewLine() => sb.AppendLine();
    }

    static string PrintTree(Node tree, int nodeWidth = 2)
    {
        string nodeValueFormat = string.Concat("{0,", nodeWidth.ToString(), "}");
        int height = Node.GetHeight(tree);
        int paddingCount = 0;
        
        var lines = new string[height + 1];
        var line = new StringBuilder();
        for (int level = height; level >= 0; level--)
        {
            var padding = new String(' ', paddingCount * nodeWidth);
            line.Append(padding);
            var spacerCount = paddingCount * 2 + 1;
            var spacer = new String(' ', spacerCount * nodeWidth);
            var nodeValues = Node.GetNodesAtLevel(tree, level).Select(n => string.Format(nodeValueFormat, n?.Value));
            line.Append(string.Join(spacer, nodeValues));
            line.Append(padding);
            lines[level] = line.ToString();
            line.Clear();
            paddingCount = spacerCount;
        }   
        
        return string.Join(Environment.NewLine, lines);
    }
    

    private static Node GetBinarySearchTree()
    {
        var root = new Node{ Value = 8 };
        var level1Left = new Node { Value = 4};
        var level1Right = new Node { Value = 10 };
        level1Left.LeftChild = new Node { Value = 2 };
        level1Left.RightChild = new Node { Value = 6};
        level1Right.RightChild = new Node { Value = 20 };
        root.LeftChild = level1Left;
        root.RightChild = level1Right;
        return root;
    }
}
