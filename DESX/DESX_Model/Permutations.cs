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
