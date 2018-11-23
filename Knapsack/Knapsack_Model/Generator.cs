using System.Collections.Generic;
using System.Security.Cryptography;

namespace Knapsack_Model
{
    public static class Generator
    {
        private static int _singleNumberBitLength = 1024;

        public static List<BigNumber> PrivateKey()
        {
            List<BigNumber> key = new List<BigNumber>(8);
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] firstNumber = new byte[_singleNumberBitLength / 8];
            rng.GetBytes(firstNumber);
            key.Add(new BigNumber(firstNumber));
            BigNumber currentSum = key[0];
            for (int i = 0; i < _singleNumberBitLength-1; i++)
            {
                
            }
            return null;
        }
        /// <summary>
        /// generates BigNumber value in given range
        /// </summary>
        /// <param name="a1">lower value in range INCLUDED in range</param>
        /// <param name="a2">higher value in range INCLUDED in range</param>
        /// <returns>Random BigNumber in range</returns>
        private static BigNumber GenerateRandomNumberInRange(BigNumber a1, BigNumber a2)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            BigNumber generatedRandom;
            do
            {
                byte[] temp = new byte[_singleNumberBitLength / 8];
                rng.GetBytes(temp);
                generatedRandom = new BigNumber(temp);
            } while (generatedRandom < a1 && generatedRandom > a2);

            return generatedRandom;
        }

    }

}