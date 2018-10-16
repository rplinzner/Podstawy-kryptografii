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
        //TODO: Check if this method is suitable for our needs
        /// <summary>
        /// Method that Permute array given as a param with permutation
        /// </summary>
        /// <param name="permutation"></param>
        /// <param name="tableToPermute"></param>
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
    }
}
