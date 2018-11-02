using System;
using System.Text;

namespace Knapsack_Model
{   //TODO Do summaries
    public static class StringHelper
    {
        /// <summary>
        /// creates int array from string
        /// </summary>
        /// <param name="str">string with numerical values separated by dots</param>
        /// <returns>int array</returns>
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
        /// <summary>
        /// creates string from int array
        /// </summary>
        /// <param name="intTable">int array</param>
        /// <returns>string with int values separated by dots</returns>
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