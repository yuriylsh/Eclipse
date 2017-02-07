using Ch02_LinkedList;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LinkedListNodeTest
    {
        [Test]
        public void Node_WhenAppendingToTail_AddsNewNodeAsTailNode()
        {
            var head = CreateLinkedList(1, 2, 3);

            head.ToCollection()
                .Should()
                .NotBeEmpty()
                .And.HaveCount(3)
                .And.Equal(1, 2, 3);
        }

        [Test]
        public void Node_RemovingHead_ShouldReturnNewHead()
        {
            var head = CreateLinkedList(1, 2, 3);

            var newHead = Node.Remove(head, 1);

            newHead.Data.Should().Be(2);
            newHead.ToCollection().Should().Equal(2, 3);
        }

        [Test]
        public void Node_RemovingNonHead_ShouldMaintainCurrentHead()
        {
            var head = CreateLinkedList(1, 2, 3);

            var newHead = Node.Remove(head, 2);

            newHead.Data.Should().Be(1);
            newHead.ToCollection().Should().Equal(1, 3);
        }

        [Test]
        public void FindingKthToLast_KisGreaterThanLinkedListLength_ReturnsZero()
        {
            var head = CreateLinkedList(1);

            int kthToLast = -1;
            head.FindKthToLast(head, 100, nodeData => kthToLast = nodeData);

            kthToLast.Should().Be(-1);
        }

        [Test]
        public void FindingKthToLast_KEqualsZero_ReturnsTail()
        {
            var head = CreateLinkedList(1, 2, 3);

            int kthToLast = -1;
            head.FindKthToLast(head, 0, nodeData => kthToLast = nodeData);

            kthToLast.Should().Be(3);
        }

        [Test]
        public void FindingKthToLast_KEqualsOne_ReturnsOneNodePriorToTail()
        {
            var head = CreateLinkedList(1, 2, 3);

            int kthToLast = -1;
            head.FindKthToLast(head, 1, nodeData => kthToLast = nodeData);

            kthToLast.Should().Be(2);
        }

        private static Node CreateLinkedList(params int[] nodeValues)
        {
            var head = new Node(nodeValues[0]);
            var currentNode = head;
            for (int i = 1; i < nodeValues.Length; i++)
            {
                currentNode = currentNode.AppendToTail(nodeValues[i]);
            }
            return head;
        }
    }
}