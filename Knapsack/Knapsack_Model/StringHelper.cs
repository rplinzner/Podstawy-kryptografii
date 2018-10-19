using System;
using System.Text;

namespace Knapsack_Model
{   //TODO Do summaries
    public static class StringHelper
    {
        public static int[] DecodeString(string str)
        {
            string[] charsEncrypted = str.Split('.');
            int[] charDecodedInt = new int[charsEncrypted.Length];
            for (int i = 0; i < charsEncrypted.Length; i++)
            {
                charDecodedInt[i] = int.Parse(charsEncrypted[i]);
            }

            return charDecodedInt;
        }

        public static string ConvertIntTableToString(int[] intTable)
        {
            StringBuilder str = new StringBuilder();
            foreach (var i in intTable)
            {
                str.Append(i);
                str.Append('.');
            }

            str.Length--; //deletes last separator

            return str.ToString();
        }

    }
}