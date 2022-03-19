using System;

namespace SecondLab
{
    internal interface ICipher
    {
        public String Encode(String toEncode)
        {
            return toEncode;
        }
        public String Decode(String toDecode)
        {
            return toDecode;
        }
    }
}
