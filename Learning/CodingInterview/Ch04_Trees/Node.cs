using System;
using System.Collections.Generic;
using System.Linq;

public class Node 
{
    public static void TraverseInOrder(Node node, Action<Node> visitor)
    {
        if (node != null)
        {
            if(node.LeftChild != null) TraverseInOrder(node.LeftChild, visitor);
            visitor(node);
            if(node.RightChild != null) TraverseInOrder(node.RightChild, visitor);
        }
    }

    internal static void TraversePreOrder(Node node, Action<Node> visitor)
    {
        if(node != null)
        {
            visitor(node);
            if(node.LeftChild != null) TraversePreOrder(node.LeftChild, visitor);
            if(node.RightChild != null) TraversePreOrder(node.RightChild, visitor);
        }
    }

    internal static void TraversePostOrder(Node node, Action<Node> visitor)
    {
        if(node != null)
        {
            if(node.LeftChild != null) TraversePreOrder(node.LeftChild, visitor);
            if(node.RightChild != null) TraversePreOrder(node.RightChild, visitor);
            visitor(node);
        }
    }

    internal static int GetHeight(Node node)
    {
        return GetLevelRecursive(node, 0);

        int GetLevelRecursive(Node currentNode, int accumulator)
        {
            if(currentNode == null) return accumulator;
            return GetLevelRecursive(currentNode.LeftChild, accumulator + 1);
        }
    }

    internal static IEnumerable<Node> GetNodesAtLevel(Node root, int level)
    {
        int height = Node.GetHeight(root);
        var nodes = new List<NodeLevel>();
        LevelNodeRecursive(root, 0);
        return nodes
            .Where(nodeLevel => nodeLevel.Level == level)
            .Select(nodeLevel => nodeLevel.Node);

        void LevelNodeRecursive(Node node, int nodeLevel){
            if(nodeLevel > height) return;
            nodes.Add(new NodeLevel{Node = node, Level = nodeLevel});
            LevelNodeRecursive(node?.LeftChild, nodeLevel + 1);
            LevelNodeRecursive(node?.RightChild, nodeLevel + 1);
        }
    }

    public int Value { get; set; }
    
    public Node LeftChild { get; set; }

    public Node RightChild { get; set; }

    private class NodeLevel
    {
        public Node Node { get; set; }
        public int Level { get; set; }
    }
}