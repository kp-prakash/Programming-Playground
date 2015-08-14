using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new TreeNode<int> { Value = 1 };
            var left1 = parent.AddLeftChild(2);
            var right1 = parent.AddRightChild(3);
            left1.AddLeftChild(4);
            left1.AddRightChild(5);
            right1.AddLeftChild(6);
            right1.AddRightChild(7);
            Console.WriteLine("In Order");
            TreeNode<int>.InOrder(parent);
            Console.WriteLine();
            Console.WriteLine("Pre Order");
            TreeNode<int>.PreOrder(parent);
            Console.WriteLine();
            Console.WriteLine("Post Order");
            TreeNode<int>.PostOrder(parent);
            Console.WriteLine();
            Console.WriteLine("DFS");
            TreeNode<int>.DepthFirstSearch(parent);
            Console.WriteLine("BFS");
            TreeNode<int>.BreadthFirstSearch(parent);
            Console.ReadLine();
        }
    }
}
