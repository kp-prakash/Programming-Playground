using System;
using System.Collections.Generic;

namespace Tree
{
    public interface ITreeNode<T>
    {
        ITreeNode<T> Parent { get; set; }
        ITreeNode<T> LeftChild { get; set; }
        ITreeNode<T> RightChild { get; set; }
        T Value { get; set; }
        ITreeNode<T> AddLeftChild(T value);
        ITreeNode<T> AddRightChild(T value);
        bool IsRootNode();
        bool IsLeafNode();
        bool HasLeftChild { get; }
        bool HasRightChild { get; }
        bool IsVisited { get; set; }
    }

    public class TreeNode<T> : ITreeNode<T>
    {
        public ITreeNode<T> Parent { get; set; }
        public ITreeNode<T> LeftChild { get; set; }
        public ITreeNode<T> RightChild { get; set; }
        public T Value { get; set; }

        public ITreeNode<T> AddLeftChild(T value)
        {
            var leftChild = new TreeNode<T> { Value = value, Parent = this };
            LeftChild = leftChild;
            return leftChild;
        }

        public ITreeNode<T> AddRightChild(T value)
        {
            var rightChild = new TreeNode<T> { Value = value, Parent = this };
            RightChild = rightChild;
            return rightChild;
        }

        public bool IsRootNode()
        {
            return Parent == null;
        }

        public bool IsLeafNode()
        {
            return LeftChild == null && RightChild == null;
        }

        public static int MaxDepth(ITreeNode<T> root)
        {
            if (null == root) return 0;
            return 1 + Math.Max(MaxDepth(root.LeftChild), MaxDepth(root.RightChild));
        }

        public static int MinDepth(ITreeNode<T> root)
        {
            if (null == root) return 0;
            return 1 + Math.Min(MinDepth(root.LeftChild), MinDepth(root.RightChild));
        }

        public static bool IsBalanced(ITreeNode<T> root)
        {
            return MaxDepth(root) - MinDepth(root) <= 1;
        }

        public static void InOrder(ITreeNode<T> root)
        {
            if (root == null) return;
            InOrder(root.LeftChild);
            Console.Write(root.Value);
            InOrder(root.RightChild);
        }

        public static void PreOrder(ITreeNode<T> root)
        {
            if (root == null) return;
            Console.Write(root.Value);
            PreOrder(root.LeftChild);
            PreOrder(root.RightChild);
        }

        public static void PostOrder(ITreeNode<T> root)
        {
            if (root == null) return;
            PostOrder(root.LeftChild);
            PostOrder(root.RightChild);
            Console.Write(root.Value);
        }

        public static void BreadthFirstSearch(ITreeNode<T> node)
        {
            var queue = new Queue<ITreeNode<T>>();
            queue.Enqueue(node);
            while (queue.Count != 0)
            {
                var nodeBeingVisited = queue.Dequeue();
                if (nodeBeingVisited.HasLeftChild)
                {
                    queue.Enqueue(nodeBeingVisited.LeftChild);
                }
                if (nodeBeingVisited.HasRightChild)
                {
                    queue.Enqueue(nodeBeingVisited.RightChild);
                }
                Console.Write(nodeBeingVisited.Value);
            }
        }

        public static void DepthFirstSearch(ITreeNode<T> node)
        {
            var stack = new Stack<ITreeNode<T>>();
            stack.Push(node);
            while (stack.Count != 0)
            {
                var currentNode = stack.Peek();
                if (!currentNode.IsVisited)
                {
                    Console.WriteLine(currentNode.Value);
                    currentNode.IsVisited = true;
                }
                if (currentNode.HasLeftChild && !currentNode.LeftChild.IsVisited)
                {
                    stack.Push(currentNode.LeftChild);
                    continue;
                }
                if (currentNode.HasRightChild && !currentNode.RightChild.IsVisited)
                {
                    stack.Push(currentNode.RightChild);
                    continue;
                }
                stack.Pop();
            }
        }

        public bool HasLeftChild
        {
            get { return LeftChild != null; }
        }

        public bool HasRightChild
        {
            get { return RightChild != null; }
        }

        public bool IsVisited { get; set; }
    }
}
