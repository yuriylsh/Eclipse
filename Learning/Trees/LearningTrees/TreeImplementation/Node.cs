using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeImplementation
{
    public class Node<T>
    {
        public Node(T value) => Value = value;

        public Node() { }

        public T Value { get; set; }

        public Node<T> LeftChild { get; set; }

        public Node<T> RightChild { get; set; }

        public static void TraverseInOrder(Node<T> node, Action<Node<T>> visitor)
        {
            if (node == null) return;
            TraverseInOrder(node.LeftChild, visitor);
            visitor(node);
            TraverseInOrder(node.RightChild, visitor);
        }

        internal static void TraversePreOrder(Node<T> node, Action<Node<T>> visitor)
        {
            if (node == null) return;
            visitor(node);
            TraversePreOrder(node.LeftChild, visitor);
            TraversePreOrder(node.RightChild, visitor);
        }

        internal static void TraversePostOrder(Node<T> node, Action<Node<T>> visitor)
        {
            if (node == null) return;
            TraversePreOrder(node.LeftChild, visitor);
            TraversePreOrder(node.RightChild, visitor);
            visitor(node);
        }

        internal static int GetHeight(Node<T> node)
        {
            return GetLevelRecursive(node, 0);

            int GetLevelRecursive(Node<T> currentNode, int accumulator) 
                => currentNode == null ? accumulator : GetLevelRecursive(currentNode.LeftChild, accumulator + 1);
        }

        public static IEnumerable<Node<T>> GetNodesAtLevel(Node<T> root, int level)
        {
            var height = GetHeight(root);
            if (level >= height) return Array.Empty<Node<T>>();

            var nodes = new List<NodeLevel>();
            LevelNodeRecursive(root, 0);
            return nodes
                .Where(nodeLevel => nodeLevel.Level == level)
                .Select(nodeLevel => nodeLevel.Node);

            void LevelNodeRecursive(Node<T> node, int nodeLevel){
                if(nodeLevel > level) return;
                nodes.Add(new NodeLevel{Node = node, Level = nodeLevel});
                LevelNodeRecursive(node?.LeftChild, nodeLevel + 1);
                LevelNodeRecursive(node?.RightChild, nodeLevel + 1);
            }
        }

        private class NodeLevel
        {
            public Node<T> Node { get; set; }
            public int Level { get; set; }
        }
    }
}