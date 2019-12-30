using System.Collections.Generic;

namespace Solutions.Shared
{
    public class Node
    {
        private readonly string _label;
        private int _depth;
        private readonly List<Node> _children = new List<Node>();
        private Node _parent;

        public IReadOnlyList<Node> Children => _children;
        public string Label => _label;
        public int Depth => _depth;
        public Node Parent => _parent;
        
        public Node(string label, int depth)
        {
            _label = label;
            _depth = depth;
        }
        
        public void AddChild(Node child)
        {
            child.SetDepth(_depth + 1);
            child._parent = this;
            _children.Add(child);
        }

        private void SetDepth(int depth)
        {
            _depth = depth;
            foreach (var child in _children)
            {
                child.SetDepth(depth + 1);
            }
        }
    }
}