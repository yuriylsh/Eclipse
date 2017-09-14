using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TreeImplementation;
using Xunit;

namespace TreesTests
{
    public class NodeTests
    {
        [Fact]
        public void GetNodesAtLevel_TreeHasNodesAtLevel0_ReturnsNodesAtLevel0()
        {
            var nodesAtLevel = Node<int>.GetNodesAtLevel(ProduceBinarySearchTree(), 0);

            nodesAtLevel.Select(GetNodeValue).Should().Equal(4);
        }

        [Fact]
        public void GetNodesAtLevel_TreeHasNodesAtLevel1_ReturnsNodesAtLevel1()
        {
            var nodesAtLevel = Node<int>.GetNodesAtLevel(ProduceBinarySearchTree(), 1);

            nodesAtLevel.Select(GetNodeValue).Should().Equal(2, 6);
        }

        [Fact]
        public void GetNodesAtLevel_TreeHasNodesAtLevel2_ReturnsNodesAtLevel2()
        {
            var nodesAtLevel = Node<int>.GetNodesAtLevel(ProduceBinarySearchTree(), 2);

            nodesAtLevel.Select(GetNodeValue).Should().Equal(1, 3, 5, 7);
        }

        [Fact]
        public void GetNodesAtLevel_TreeHasNodesAtRequestedLevel_ReturnsEmptyArray()
        {
            var nodesAtLevel = Node<int>.GetNodesAtLevel(ProduceBinarySearchTree(), 999);

            nodesAtLevel.Should().BeEmpty();
        }

        [Fact]
        public void TraverseInOrder_GivenBinarySearchTree_TraversesNodesInSortOrder()
        {
            var tree = ProduceBinarySearchTree();
            var result = new List<int>();
            void Visitor(Node<int> node) => result.Add(node.Value);

            Node<int>.TraverseInOrder(tree, Visitor);

            result.Should().Equal(1, 2, 3, 4, 5, 6, 7);
        }

        private static int GetNodeValue(Node<int> node) => node.Value;

        private static Node<int> ProduceBinarySearchTree()
        {
            var three = new Node<int>(3);
            var one = new Node<int>(1);
            var two = new Node<int>(2) { LeftChild = one, RightChild = three };
            var five = new Node<int>(5);
            var seven = new Node<int>(7);
            var six = new Node<int>(6) { LeftChild = five, RightChild = seven };
            var root = new Node<int>(4) { LeftChild = two, RightChild = six };
            return root;
        }
    }
}
