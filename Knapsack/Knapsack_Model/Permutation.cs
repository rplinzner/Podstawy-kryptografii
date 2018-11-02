using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Model
{
    public static class Permutation
    {
        /// <summary>
        /// Method that Permute array given as a param with permutation
        /// </summary>
        /// <param name="permutation">permutation pattern</param>
        /// <param name="tableToPermute">table to perform permutation on</param>
        /// <returns>permuted int array</returns>
        public static int[] Permute(byte[] permutation, int[] tableToPermute)
        {
            int[] permutedInts = new int[permutation.Length];

            for (int i = 0; i < permutation.Length; i++)
            {
                permutedInts[i] = tableToPermute[permutation[i] - 1];
            }
            
            return permutedInts;
        }
        /// <summary>
        /// Method that Permute array given as a param with permutation 
        /// </summary>
        /// <param name="permutation">permutation pattern</param>
        /// <param name="tableToPermute">table to perform permutation on</param>
        /// <returns>permuted BitArray array</returns>
        public static BitArray PermuteBitArray(byte[] permutation, BitArray tableToPermute)
        {
            BitArray permutedBits = new BitArray(tableToPermute.Length);

            for (int i = 0; i < permutation.Length; i++)
            {
                permutedBits[i] = tableToPermute[permutation[i] - 1];
            }

            return permutedBits;
        }
    }
}
