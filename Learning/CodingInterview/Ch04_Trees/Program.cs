using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        var tree = CreateBinarySearchTree(5);
        var sb = new StringBuilder();

        sb.Append(PrintTree(tree));
        NewLine();
        sb.Append("In order traversal: ");
        Node.TraverseInOrder(tree, PrintVisitor);
        NewLine();
        sb.Append("Pre order traversal: " );
        Node.TraversePreOrder(tree, PrintVisitor);
        NewLine();
        int height = Node.GetHeight(tree);
        sb.Append("Tree height: ").Append(height);
        NewLine();
        
        int numberOfNodes = 0;
        Node.TraverseInOrder(tree, node => numberOfNodes++);
        sb.Append("Number of nodes: ").Append(numberOfNodes);
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

    private static Node CreateBinarySearchTree(int oneBasedHeight)
    {
        
        int numberOfNodes = (1 << oneBasedHeight) - 1;
        var nodeValues = GetNodeValues(numberOfNodes, 1, numberOfNodes < 99 ? 99 : numberOfNodes);

        var root = new Node{ Value = nodeValues[0]};
        for(var i = 1; i < numberOfNodes - 1; i++){
            Add(root, nodeValues[i]);
        }

        return root;

            void Add(Node node, int item)
            {
                if(item < node.Value){
                    if(node.LeftChild == null) node.LeftChild = new Node{ Value = item};
                    else Add(node.LeftChild, item);
                }else{
                    if(node.RightChild == null) node.RightChild = new Node { Value = item };
                    else Add(node.RightChild, item);
                }
            }

        
            int[] GetNodeValues(int n, int minValue, int maxValue)
            {
                var rnd = new Random((int)DateTime.Now.Ticks);
                var result = new int[n];
                for (int i = 0; i < n; i++)
                {
                    var newItem = rnd.Next(minValue, maxValue);
                    while(result.Contains(newItem)) newItem = rnd.Next(minValue, maxValue);
                    result[i] = newItem;
                }
                return result;
            }
    }
}
