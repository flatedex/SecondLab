using System;
using System.Collections.Generic;

namespace SecondLab
{
    public class Atbash : ICipher
    {
        public String Encode(String text)
        {
            List<int> codes = new List<int>();
            String newText = "";
            foreach (char letter in text)
            {
                codes.Add((int)letter);
            }

            for (int i = 0; i < codes.Count; i++)
            {
                //changes if uppercase
                if (codes[i] >= 65 & codes[i] <= 90)
                {
                    int shift = 90 - codes[i]; // 90 is code of the last upper latin letter in UTF-8
                    codes[i] = 65 + shift; //65 is code of the first upper latin letter in UTF-8
                }
                //changes if lowercase
                else if (codes[i] >= 97 & codes[i] <= 122)
                {
                    int shift = 122 - codes[i]; // 122 is code of the last lower latin letter in UTF-8
                    codes[i] = 97 + shift; //97 is code of the first lower latin letter in UTF-8`
                }
                newText += (char)codes[i];
            }
            return newText;
        }

        public String Decode(String text)
        {
            return Encode(text);
        }
    }
}