using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Shared
{
    public static class Tree
    {
        public static Node Build(IEnumerable<string> input, string root = "COM")
            => Build(input.Select(x => x.Split(')')).Select(x => (x[0], x[1])), root);
        public static Node Build(IEnumerable<(string, string)> input, string root = "COM")
        {
            var getNode = GetOrAddNode(new Dictionary<string, Node>());
            foreach (var (parentLabel, childLabel) in input)
            {
                getNode(parentLabel).AddChild(getNode(childLabel));
            }
            return getNode(root);
        }
        
        private static Func<string, Node> GetOrAddNode(IDictionary<string, Node> cache)
            => node =>
            {
                {
                    if (cache.TryGetValue(node, out var result)) return result;
                    result = new Node(node, 0);
                    cache.Add(node, result);
                    return result;
                }
            };


        public static void DepthFirstPreorder(Node root, Action<Node> visit) => DepthFirstPreorder(root, x =>
            {
                visit(x);
                return false;
            });
        
        private static void DepthFirstPreorder(Node root, Func<Node, bool> visit)
        {
            if (visit(root)) return;
            foreach (var child in root.Children)
            {
                DepthFirstPreorder(child, visit);
            }
        }

        public static Node GetByLabel(Node root, string label)
        {
            Node result = null;
            DepthFirstPreorder(root, x =>
            {
                if (x.Label != label) return false;
                result = x;
                return true;
            });
            
            return result;
        }

        public static Node LowestCommonAncestor(Node a, Node b)
        {
            static HashSet<string> Path(Node node)
            {
                var result = new HashSet<string>();
                var current = node.Parent;
                while (current != null)
                {
                    result.Add(current.Label);
                    current = current.Parent;
                }

                return result;
            }

            var aPath = Path(a);
            var parent = b.Parent;
            while (parent != null)
            {
                if (aPath.Contains(parent.Label)) return parent;
                parent = parent.Parent;
            }

            return null;
        }
    }
}