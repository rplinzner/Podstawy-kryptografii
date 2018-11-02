using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Model
{
    /// <summary>
    /// Default, working values for encrytion/decryption
    /// </summary>
    public static class DefaultValues
    {
        /// <summary>
        /// A superincreasing sequence is one in which the next term of the sequence is greater than the sum of all preceding terms.
        /// </summary>
        /// <returns>8-element superincreasing sequence</returns>
        public static int[] PrivateKey()
        {
            int[] temp = {7, 11, 19, 39, 79, 157, 313, 628}; //8 elements, as we use utf-8 encoding
            return temp;
        }

        /// <summary>
        /// The modulus should be a number greater than the sum of all the numbers in the superincreasing sequence
        /// </summary>
        /// <returns>Modulus</returns>
        public static int Modulus()
        {
            return 1300;
        }

        /// <summary>
        /// The multiplier should have no factors in common with the modulus so prime numbers works best.
        /// Also should be greater than Modulus/2 and lower then Modulus
        /// </summary>
        /// <returns>Multiplier</returns>
        public static int Multiplier()
        {
            return 659;
        }

        /// <summary>
        /// PermutationTable table, that has to contain permutation for 8 bits, as we're using UTF-8
        /// </summary>
        /// <returns>8-bit permutation table</returns>
        public static byte[] PermutationTable()
        {
            byte[] temp = {5, 2, 8, 1, 7, 4, 6, 3};
            return temp;
        }

    }
}
