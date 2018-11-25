using System;
using System.Collections.Generic;
using System.Text;

namespace Knapsack_Model
{   //TODO Do summaries
    public static class StringHelper
    {
        /// <summary>
        /// creates big number list from string
        /// </summary>
        /// <param name="str">string with numerical values separated by dots</param>
        /// <returns>big number array</returns>
        public static List<BigNumber> DecodeString(string str)
        {
            string[] charsEncrypted = str.Split('.');
            List<BigNumber> charDecodedBigNumbers = new List<BigNumber>(charsEncrypted.Length);
            for (int i = 0; i < charsEncrypted.Length; i++)
            {
                //parse from string, hex
                charDecodedBigNumbers.Add(new BigNumber(charsEncrypted[i],16));
            }

            return charDecodedBigNumbers;
        }
        /// <summary>
        /// creates string from BigNumber List
        /// </summary>
        /// <param name="list">BigNumber list</param>
        /// <returns>string with BN values separated by dots</returns>
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