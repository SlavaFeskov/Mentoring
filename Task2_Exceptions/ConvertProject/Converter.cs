using System;
using System.ComponentModel;

namespace ConvertProject
{
    public static class Converter
    {
        public static int ConvertString(string inputString)
        {
            int result = 0;
            for (var i = 0; i < inputString.Length; i++)
            {
                if (!char.IsNumber(inputString[i]))
                {
                    throw new StringConverterException(inputString, i);
                }

                var symbolAsInt =
                    (int) (char.GetNumericValue(inputString[i]) * Math.Pow(10, inputString.Length - 1 - i));
                result += symbolAsInt;
            }

            return result;
        }
    }
}