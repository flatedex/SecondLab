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
        public static String KeyboardInput(String toEncode) // Create list before random for saving inputs
        {
            Console.WriteLine("Enter a string you want to encode:");
            toEncode = Console.ReadLine().ToString();
            Console.WriteLine("\nDone successfully!\n");
            return toEncode;
        }
        public static void KeyboardCoefficients(ref int a, ref int b)
        {
            Boolean isOk = false;
            const int lengthOfAlphabet = 26;
            do
            {
                Console.WriteLine("Enter coefficient a:");
                a = GetNumber();
                while (!Affine.IsCoprime(a, lengthOfAlphabet))
                {
                    Console.WriteLine("Coefficient a must be coprime with length of alphabet");
                    Console.WriteLine("Enter coefficient a:");
                    a = GetNumber();
                }
                Console.WriteLine("Enter coefficient b:");
                b = GetNumber();
                Console.WriteLine("\nDone successfully!\n");
                isOk = true;
            } while (!isOk);
        }

        public static string RandomInput(String toEncode) // Create list before random for saving inputs
        {
            
            const int leftLower = 97;
            const int rightLower = 122;

            Console.WriteLine("How many letters in lower case do you want to add?");
            int counter = GetNumber();
            Random random = new Random();
            for (int i = 0; i <= counter - 1; i++)
            {
                Char letter = (Char)random.Next(leftLower, rightLower);
                toEncode += letter;
            }
            Console.WriteLine(toEncode);
            Console.WriteLine("\nDone successfully!\n");
            return toEncode;
        }
    }
}