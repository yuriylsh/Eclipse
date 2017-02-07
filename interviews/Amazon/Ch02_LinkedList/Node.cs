using System;
using System.Collections;
using System.Collections.Generic;

namespace Ch02_LinkedList
{
    public class Node
    {
        private Node _next;

        public Node(int data)
        {
            Data = data;
        }

        public int Data { get; }

        public Node AppendToTail(int data)
        {
            var tailNode = FindTailNode();

            var newNode = new Node(data);
            tailNode._next = newNode;

            return newNode;
        }

        private Node FindTailNode()
        {
            var tailNode = this;
            while (tailNode._next != null)
            {
                tailNode = tailNode._next;
            }
            return tailNode;
        }

        public void IterateUntilEnd(Action<int> visitor)
        {
            var currentNode = this;
            while (true)
            {
                visitor(currentNode.Data);
                if (currentNode._next == null) break;
                currentNode = currentNode._next;
            }
        }

        public ICollection<int> ToCollection()
        {
            var result = new List<int>();
            IterateUntilEnd(nodeData => result.Add(nodeData));
            return result;
        }

        public static Node Remove(Node head, int data)
        {
            Node newHead = head;
            if (head.Data == data)
            {
                newHead = head._next;
            }
            else
            {
                RemoveNonHeadNode(head, data);
            }
            return newHead;
        }

        private static void RemoveNonHeadNode(Node startingNode, int data)
        {
            while (true)
            {
                if (startingNode._next?.Data == data)
                {
                    startingNode._next = startingNode._next._next;
                    break;
                }
                startingNode = startingNode?._next;
                if (startingNode == null) break;
            }
        }

        // This algorithm recurses through the linked list. 
        // When it hits the end, the method passes back a counter set to 0. 
        // Each parent call adds 1 to this counter. 
        // When the counter equalsk , we know we have reached thek th to last element of the linked list. 
        public int FindKthToLast(Node head, int k, Action<int> kthToLastVisitor)
        {
            if (head._next == null)
            {
                if (k == 0) kthToLastVisitor(head.Data);
                return 0;
            }


            var index = FindKthToLast(head._next, k, kthToLastVisitor) + 1;
            if (index == k) kthToLastVisitor(head.Data);

            return index;
        }
        
    }
}
