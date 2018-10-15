using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESX_Model
{
    public static class Permutations
    {
        /// <summary>
        /// Table for initial bit permutation for single plain text block
        /// </summary>
        public static byte[] InitialPermutation =
        {
            58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7
        };

        /// <summary>
        /// Method that Permute array given as a param with permutation
        /// </summary>
        /// <param name="permutation"></param>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static BitArray Permute(byte[] permutation, BitArray byteArray)
        {
            BitArray bits=new BitArray(byteArray);

            BitArray permutedBitArray = new BitArray(permutation.Length);
            for (int i = 0; i < permutation.Length; i++)
            {
                permutedBitArray[i] = bits[permutation[i]-1];
            }
           // byte[] permutatedByteArray = new byte[(permutedBitArray.Length - 1) / 8 + 1];
           // permutedBitArray.CopyTo(permutatedByteArray, 0);
            //TODO: Issue: #3 Test Permute Method
            return permutedBitArray;
        }

        public static BitArray Permute(byte[,] permutation, BitArray bitArray)
        {
            BitArray rowBitArray= new BitArray(new bool[]{bitArray[0],bitArray[5]});
            BitArray columndBitArray= new BitArray(new bool[] { bitArray[1], bitArray[2], bitArray[3], bitArray[4] });
            int row = rowBitArray.ToInt();
            int column = rowBitArray.ToInt();
            byte value = permutation[row, column];
            return new BitArray(new byte[]{value});
        }
    }
}
