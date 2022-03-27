using System;
using System.Linq;
using System.Collections.Generic;

namespace SecondLab
{
    public class Affine : ICipher
    {
        public int lengthOfAlphabet = 26;

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
                if (codes[i] >= 65 & codes[i] <= 90)
                {
                    int x = Convert.ToInt32(codes[i] - 65);
                    coded += (char)(((a * x + b) % 26) + 65);
                }
                //changes if lowercase
                else if (codes[i] >= 97 & codes[i] <= 122)
                {
                    int x = Convert.ToInt32(codes[i] - 97);
                    coded += (char)(((a * x + b) % 26) + 97);
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

            int aInvers = MultiplicativeInverse(a);

            List<int> codes = new List<int>();
            String decoded = "";

            foreach (char letter in cipheredText)
            {
                codes.Add((int)letter);
            }

            for (int i = 0; i < codes.Count; i++)
            {
                //changes if uppercase
                if (codes[i] >= 65 & codes[i] <= 90)
                {
                    int x = Convert.ToInt32(codes[i] - 65);
                    if (x - b < 0)
                    {
                        x += Convert.ToInt32(x) + 26;
                    }
                    decoded += (char)((aInvers * (x-b) % 26) + 65);
                }
                //changes if lowercase
                else if (codes[i] >= 97 & codes[i] <= 122)
                {
                    int x = Convert.ToInt32(codes[i] - 97);
                    if (x - b < 0)
                    {
                        x += Convert.ToInt32(x) + 26;
                    }
                    if (((char)(aInvers * (x - b) % 26) + 97) == 'f') { decoded += 'k'; }
                    else decoded += (char)((aInvers * (x - b) % 26) + 97);
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