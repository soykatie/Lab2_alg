using System;

namespace Lab2_Ukleiko
{
    public static class TasksLab2
    {
        static void Main(string[] args)
        {
            bool isInt = false;
            do
            {
                try
                {
                    Console.Title = ("MENU");
                    Console.Write("Lab 2, Ukleiko Ekaterina, 2 group, 3 course\n\n");
                    Console.Write("MENU\n\n");
                    Console.Write("Enter 1 - Task 1: binary & interpolation searches\n");
                    Console.Write("Enter 2 - Task 2: algorithms for BST\n");
                    Console.Write("Enter 3 - Task 3: hashing\n");
                    Console.Write("Enter integer number, please: ");
                    int userChoice = Int32.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (userChoice)
                    {
                        case 1:
                            try
                            {
                                Console.Title = ("TASK 1: BINARY & INTERPOLATION SEARCH");
                                Console.Write("Enter N - array length: ");
                                int N1 = int.Parse(Console.ReadLine());

                                Console.Write("Enter M - the upper limit of numbers in array: ");
                                int M1 = int.Parse(Console.ReadLine());

                                int[] arr1 = BasicMethods.GenerateRandArr(N1, M1);

                                Console.WriteLine("\nSource array: ");
                                BasicMethods.Print(arr1);

                                Console.Write("Enter x - the number you want to find: ");
                                int x = int.Parse(Console.ReadLine());

                                int numOfCompsBinary = Search.BinarySearch(arr1, x);
                                int numOfCompsInterpolation = Search.InterpolationSearch(arr1, x);

                                Console.WriteLine("\nNumber of comparisons (binary search) = " + numOfCompsBinary);
                                Console.WriteLine("\nNumber of comparisons (interpolation search) = " + numOfCompsInterpolation);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                Console.Title = ("TASK 2: BST");
                                BinarySearchTree tree = new BinarySearchTree();

                                int[] sequence = { 5, 3, 8, 2, 4, 7, 9 };
                                foreach (int num in sequence)
                                {
                                    tree.Insert(num);
                                }

                                Console.WriteLine("Inorder traversal:");
                                tree.Inorder();

                                Console.WriteLine("\n\nReverse Inorder traversal:");
                                tree.ReverseInorder();

                                int k = 3;
                                Console.WriteLine($"\n\n{k}-th smallest element in the tree: {tree.KthSmallest(k)}");

                                tree.BalanceTree();
                                Console.WriteLine("\n\nAfter balancing the tree:");
                                tree.Inorder();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                        case 3:
                            try
                            {
                                Console.Title = ("TASK 3: HASHING");
                                int[] testData = { 23, 47, 32, 55, 78, 46, 71, 89, 16, 38 };
                                int size = 11;
                                HashTable table = new HashTable(size);

                                int knuthConstant = HashTable.KnuthConstant(size);
                                Console.WriteLine("Knuth Constant: " + knuthConstant);
                                foreach (int key in testData)
                                {
                                    table.Insert(key * knuthConstant);
                                }

                                Console.WriteLine("Maximum chain length using Knuth Constant: " + HashTable.CalculateChainLength(table));

                                int multiplicationConstant = HashTable.MultiplicationConstant(size);
                                Console.WriteLine("Multiplication Constant: " + multiplicationConstant);
                                foreach (int key in testData)
                                {
                                    table.Insert(key * multiplicationConstant);
                                }

                                Console.WriteLine("Maximum chain length using Multiplication Constant: " + HashTable.CalculateChainLength(table));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR: " + e.Message);
                            }
                            break;
                    }
                    Console.ReadKey();
                    isInt = true;
                }
                catch
                {
                    Console.Write("You've entered not an integer number! Try again!\n");
                }
            } while (!isInt);
        }
        public static class BasicMethods
        {
            public static int[] GenerateRandArr(int N, int M)
            {
                int[] arr = new int[N];
                Random rand = new Random();

                for (int i = 0; i < N; i++)
                {
                    arr[i] = rand.Next(0, M + 1);
                }

                return arr;
            }
            public static void Print(int[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.WriteLine(string.Join(", ", arr[i]));
                }
            }
        }

        public static class Search
        {
            public static int BinarySearch(int[] arr, int x)
            {
                int lhs = 0;
                int rhs = arr.Length - 1;
                int numOfComps = 0; //число операций сравнения

                while (lhs <= rhs)
                {
                    int mid = lhs + (rhs - lhs) / 2;

                    if (arr[mid] == x)
                    {
                        numOfComps++;
                        return numOfComps;
                    }
                    else if (arr[mid] < x)
                    {
                        lhs = mid + 1;
                    }
                    else
                    {
                        rhs = mid - 1;
                    }
                    numOfComps++;
                }

                return numOfComps;
            }

            public static int InterpolationSearch(int[] arr, int x)
            {
                int low = 0;
                int high = arr.Length - 1;
                int numOfComps = 0;

                while (low <= high && x >= arr[low] && x <= arr[high])
                {
                    int pos = low + ((x - arr[low]) * (high - low)) / (arr[high] - arr[low]);

                    if (arr[pos] == x)
                    {
                        numOfComps++;
                        return numOfComps;
                    }
                    else if (arr[pos] < x)
                    {
                        low = pos + 1;
                    }
                    else
                    {
                        high = pos - 1;
                    }
                    numOfComps++;
                }

                return numOfComps;
            }
        }

        public class Node
        {
            public int val;
            public Node left, right;

            public Node(int item)
            {
                val = item;
                left = right = null;
            }
        }

        public class BinarySearchTree
        {
            Node root;

            public BinarySearchTree()
            {
                root = null;
            }

            public void Insert(int value)
            {
                root = InsertRec(root, value);
            }

            Node InsertRec(Node root, int value)
            {
                if (root == null)
                {
                    root = new Node(value);
                    return root;
                }

                if (value < root.val)
                {
                    root.left = InsertRec(root.left, value);
                }
                else if (value > root.val)
                {
                    root.right = InsertRec(root.right, value);
                }

                return root;
            }

            public void Inorder()
            {
                InorderRec(root);
            }

            public void InorderRec(Node root)
            {
                if (root != null)
                {
                    InorderRec(root.left);
                    Console.Write(root.val + " ");
                    InorderRec(root.right);
                }
            }

            public void ReverseInorder()
            {
                ReverseInorderRec(root);
            }

            public void ReverseInorderRec(Node root)
            {
                if (root != null)
                {
                    ReverseInorderRec(root.right);
                    Console.Write(root.val + " ");
                    ReverseInorderRec(root.left);
                }
            }

            public int KthSmallest(int k)
            {
                int[] count = { 0 };
                return KthSmallestRec(root, k, count);
            }

            public int KthSmallestRec(Node root, int k, int[] count)
            {
                if (root == null)
                {
                    return int.MinValue;
                }

                int left = KthSmallestRec(root.left, k, count);
                if (left != int.MinValue)
                {
                    return left;
                }

                count[0]++;
                if (count[0] == k)
                {
                    return root.val;
                }

                return KthSmallestRec(root.right, k, count);
            }

            public int CountNodes(Node root)
            {
                if (root == null)
                {
                    return 0;
                }
                return 1 + CountNodes(root.left) + CountNodes(root.right);
            }

            public void BalanceTree()
            {
                int n = CountNodes(root);
                BalanceTreeRec(root, n);
            }

            Node BalanceTreeRec(Node root, int n)
            {
                if (root == null)
                {
                    return null;
                }

                BalanceTreeRec(root.left, n);
                BalanceTreeRec(root.right, n);

                int k = (n / 2) + 1;
                int kth = KthSmallest(k);

                Delete(root, kth);
                Insert(kth);

                return root;
            }

            public void Delete(Node root, int key)
            {
                Node parent = null;
                Node current = root;

                while (current != null && current.val != key)
                {
                    parent = current;
                    if (key < current.val)
                    {
                        current = current.left;
                    }
                    else
                    {
                        current = current.right;
                    }
                }

                if (current == null)
                {
                    return;
                }

                if (current.left == null && current.right == null)
                {
                    if (current != root)
                    {
                        if (parent.left == current)
                        {
                            parent.left = null;
                        }
                        else
                        {
                            parent.right = null;
                        }
                    }
                    else
                    {
                        root = null;
                    }
                }
                else if (current.left != null && current.right != null)
                {
                    Node successor = FindSuccessor(current.right);
                    int temp = successor.val;
                    Delete(root, successor.val);
                    current.val = temp;
                }
                else
                {
                    Node child = (current.left != null) ? current.left : current.right;
                    if (current != root)
                    {
                        if (current == parent.left)
                        {
                            parent.left = child;
                        }
                        else
                        {
                            parent.right = child;
                        }
                    }
                    else
                    {
                        root = child;
                    }
                }
            }

            Node FindSuccessor(Node root)
            {
                while (root.left != null)
                {
                    root = root.left;
                }
                return root;
            }
        }
        public class HashTable
        {
            private int size;
            private int[] data;
            private bool[] isOccupied;

            public HashTable(int size)
            {
                this.size = size;
                data = new int[size];
                isOccupied = new bool[size];
            }

            public int HashFunction(int key)
            {
                return key % size;
            }

            public int CollisionResolutionChain(int key, int index)
            {
                while (isOccupied[index])
                {
                    if (data[index] == key)
                    {
                        return index;
                    }
                    index = (index + 1) % size; 
                }
                return -1; 
            }

            public void Insert(int key)
            {
                int index = HashFunction(key);
                if (isOccupied[index])
                {
                    index = CollisionResolutionChain(key, index);
                }
                data[index] = key;
                isOccupied[index] = true;
            }

            public static int KnuthConstant(int size)
            {
                int prime = GetNextPrime(size);
                return prime - (int)(0.01 * prime);
            }

            public static int MultiplicationConstant(int size)
            {
                double A = 0.6180339887;
                return (int)(size * (A - Math.Floor(A)));
            }

            public static int GetNextPrime(int num)
            {
                while (!IsPrime(num))
                {
                    num++;
                }
                return num;
            }

            public static bool IsPrime(int num)
            {
                if (num <= 1)
                {
                    return false;
                }
                for (int i = 2; i <= Math.Sqrt(num); i++)
                {
                    if (num % i == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            public static int CalculateChainLength(HashTable table)
            {
                int maxChainLength = 0;
                int currentChainLength = 0;
                for (int i = 0; i < table.size; i++)
                {
                    if (table.isOccupied[i])
                    {
                        currentChainLength++;
                    }
                    else
                    {
                        if (currentChainLength > maxChainLength)
                        {
                            maxChainLength = currentChainLength;
                        }
                        currentChainLength = 0;
                    }
                }
                return maxChainLength;
            }
        }
    }
}