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
        for (int level = 0; level < height; level++)
        {
            var nodesAtLevel = Node.GetNodesAtLevel(tree, level);
            string nodes = string.Join(",", nodesAtLevel.Select(node => node?.Value.ToString()?? " "));
            sb.Append("Nodes at level " ).Append(level).Append(": ").Append(nodes);
            NewLine();
        }
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
        
        var result = new StringBuilder();
        
        for (int level = height; level >= 0; level--)
        {
            var nodes = Node.GetNodesAtLevel(tree, level);
            var padding = new String(' ', paddingCount * nodeWidth);
            var spacerCount = paddingCount * 2 + 1;
            var spacer = new String(' ', spacerCount * nodeWidth);
            result.Append(padding);
            var nodeValues = nodes.Select(n => string.Format(nodeValueFormat, n?.Value));
            result.Append(string.Join(spacer, nodeValues));
            result.Append(padding);
            result.Append(Environment.NewLine);
            paddingCount = spacerCount;
        }   
        
        return result.ToString();
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
