﻿using System;
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
        /// <param name="table"></param>
        /// <returns></returns>
        public static byte[] Permute(byte[] permutation, byte[] byteArray)
        {
            BitArray bits=new BitArray(byteArray);

            BitArray permutedBitArray = new BitArray(bits.Length);
            for (int i = 0; i < permutation.Length; i++)
            {
                permutedBitArray[i] = bits[permutation[i]];
            }
            byte[] permutatedByteArray = new byte[(permutedBitArray.Length - 1) / 8 + 1];
            permutedBitArray.CopyTo(permutatedByteArray, 0);
            //TODO: Issue: #3 Test Permute Method
            return permutatedByteArray;
        }
    }
}
