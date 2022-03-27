using System;
using System.Collections.Generic;

namespace SecondLab
{
    class Input
    {
        public static int GetNumber()
        {
            int num = 0;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out num))
                {
                    break;
                }
                Console.WriteLine("Unwanted number, try again");
            }
            return num;
        }
        public static int GetNumber(int a, int b)
        {
            int num = GetNumber();
            while (num < a || num > b)
            {
                Console.WriteLine($"Enter a number between {a} and {b}");
                num = GetNumber();
            }

            return num;
        }
        public List<Int32> GetArray(String str)
        {
            String[] subs = str.Split(' ');
            List<Int32> numbers = new List<int>();
            if (subs != null)
            {
                foreach (String sub in subs)
                {
                    if (Int32.TryParse(sub, out int number))
                    {
                        numbers.Add(number);
                    }
                }
            }
            return numbers;
        }
        public static void KeyboardInput(Tree tree, List<int> temp) // Create list before random for saving inputs
        {
            Console.WriteLine("How many nodes do you want to add?");
            int counter = GetNumber();
            int node = 0;
            for (int i = 0; i <= counter - 1; i++)
            {
                Console.WriteLine($"Enter node {i + 1} key");
                node = GetNumber();
                tree.InsertNode(node);
            }
            Console.WriteLine("Tree is filled");
        }
        public static void RandomInput(Tree tree, List<int> temp) // Create list before random for saving inputs
        {

            const int RIGHT = 99;
            const int LEFT = 0;

            Console.WriteLine("How many nodes do you want to add?");
            int counter = GetNumber();
            Random random = new Random();
            int node = 0;
            for (int i = 0; i <= counter - 1; i++)
            {
                node = random.Next(LEFT, RIGHT);
                temp.Add(node);
                tree.InsertNode(node);
                Console.WriteLine($"Value {node} was inserted");
            }
            Console.WriteLine("Tree is filled");
        }
    }
}