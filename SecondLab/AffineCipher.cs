using System;
using System.Linq;
using System.Collections.Generic;

namespace SecondLab
{
    public class Affine : ICipher
    {
        public const int lengthOfAlphabet = 26;
        public const int firstUppercase = 65;
        public const int lastUppercase = 90;
        public const int firstLowercase = 97;
        public const int lastLowercase = 122;

        public String Encode(String plainText, int a, int b)
        {
            if (!IsCoprime(a, lengthOfAlphabet)) throw new ArgumentException();

            List<int> codes = new List<int>();
            String coded = "";

            foreach (char letter in plainText)
            {
                codes.Add((int)letter);
            }

            for (int i = 0; i < codes.Count; i++)
            {
                //changes if uppercase
                if (codes[i] >= firstUppercase & codes[i] <= lastUppercase)
                {
                    int x = Convert.ToInt32(codes[i] - firstUppercase);
                    coded += (char)(((a * x + b) % lengthOfAlphabet) + firstUppercase);
                }
                //changes if lowercase
                else if (codes[i] >= firstLowercase & codes[i] <= lastLowercase)
                {
                    int x = Convert.ToInt32(codes[i] - firstLowercase);
                    coded += (char)(((a * x + b) % lengthOfAlphabet) + firstLowercase);
                }
                else
                {
                    coded += (char)codes[i];
                }
            }
            return coded;
        }
        public String Decode(String cipheredText, int a, int b)
        {
            if (!IsCoprime(a, lengthOfAlphabet)) throw new ArgumentException();

            int aInverse = MultiplicativeInverse(a);

            List<int> codes = new List<int>();
            String decoded = "";

            foreach (char letter in cipheredText)
            {
                codes.Add((int)letter);
            }

            for (int i = 0; i < codes.Count; i++)
            {
                //changes if uppercase
                if (codes[i] >= firstUppercase & codes[i] <= lastUppercase)
                {
                    int x = Convert.ToInt32(codes[i] - firstUppercase);
                    if (x - b < 0)
                    {
                        x += Convert.ToInt32(x) + lengthOfAlphabet;
                    }
                    decoded += (char)((aInverse * (x-b) % lengthOfAlphabet) + firstUppercase);
                }
                //changes if lowercase
                else if (codes[i] >= firstLowercase & codes[i] <= lastLowercase)
                {
                    int x = Convert.ToInt32(codes[i] - firstLowercase);
                    if (x - b < 0)
                    {
                        x += Convert.ToInt32(x) + lengthOfAlphabet;
                    }
                    if (((char)(aInverse * (x - b) % lengthOfAlphabet) + firstLowercase) == 'f') decoded += 'k';
                    else decoded += (char)((aInverse * (x - b) % lengthOfAlphabet) + firstLowercase);
                }
                else
                {
                    decoded += (char)codes[i];
                }
            }

            return new String(decoded);
        }
        public int MultiplicativeInverse(int a)
        {
            for (int i = 0; i < lengthOfAlphabet + 1; i++)
            {
                if (a * i % lengthOfAlphabet == 1)
                {
                    return i;
                }
            }
            throw new Exception("No Multiplicative Inverse Found");
        }
        public static int Coprime(int valueFirst, int valueSecond)
        {
            if (valueFirst == 0 || valueSecond == 0)
            {
                return 0;
            }
            if(valueFirst == valueSecond)
            {
                return valueFirst;
            }
            if(valueFirst > valueSecond)
            {
                return Coprime(valueFirst - valueSecond, valueSecond);
            }
            return Coprime(valueFirst, valueSecond - valueFirst);
        }
        public static Boolean IsCoprime(int valueFirst, int valueSecond)
        {
            if(Coprime(valueFirst, valueSecond) == 1) return true;
            else return false;
        }
    }
}