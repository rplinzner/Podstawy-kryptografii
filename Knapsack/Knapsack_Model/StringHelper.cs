using System;
using System.Collections.Generic;
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
        public static List<BigNumber> DecodeString(string str)
        {
            string[] charsEncrypted = str.Split('.');
//            int[] charDecodedInt = new int[charsEncrypted.Length];
            List<BigNumber> charDecodedBigNumbers = new List<BigNumber>(charsEncrypted.Length);
            for (int i = 0; i < charsEncrypted.Length; i++)
            {
                //charDecodedInt[i] = int.Parse(charsEncrypted[i]);
                charDecodedBigNumbers.Add(new BigNumber(charsEncrypted[i],16));
            }

            return charDecodedBigNumbers;
        }
        /// <summary>
        /// creates string from int array
        /// </summary>
        /// <param name="intTable">int array</param>
        /// <returns>string with int values separated by dots</returns>
        public static string ConvertBigNumberListToString(List<BigNumber> list)
        {
            StringBuilder str = new StringBuilder();
            foreach (var i in list)
            {
                str.Append(i.ToHexString());
                str.Append('.');
            }
            str.Length--; //deletes last separator

            return str.ToString();
        }

    }
}