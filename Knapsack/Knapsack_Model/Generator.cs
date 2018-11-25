using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Knapsack_Model
{
    public static class Generator
    {
        private static int _singleNumberBitLength = 1024;
        private static int _keyLength = 8;
        /// <summary>
        /// Private key Generator for given length
        /// </summary>
        /// <returns>BigNumber List key</returns>
        public static List<BigNumber> PrivateKey()
        {
            List<BigNumber> key = new List<BigNumber>(8);
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] firstNumber = new byte[_singleNumberBitLength / 8];
            rng.GetBytes(firstNumber);
            key.Add(new BigNumber(firstNumber));
            BigNumber currentSum = key[0];
            for (int i = 0; i < _keyLength - 1; i++)
            {
                BigNumber temp = GenerateRandomNumberInRange(currentSum + 1, 2 * currentSum);
                currentSum += temp;
                key.Add(temp);
            }
            return key;
        }
        /// <summary>
        /// Generates modulus, where it should be a number greater than the sum of
        /// all the numbers in the superincreasing sequence.
        /// </summary>
        /// <param name="key">SuperIncreasing sequence key</param>
        /// <returns>BigNumber Modulus</returns>
        public static BigNumber Modulus(List<BigNumber> key)
        {
            BigNumber temp = GenerateRandomNumberInRange(SumSupersequence(key) + 1, 2 * SumSupersequence(key));
            return temp;
        }
        /// <summary>
        /// Generates multiplier where should have no factors in common with the modulus (is co-prime).
        /// Also should be greater than Modulus/2 and lower then Modulus
        /// </summary>
        /// <param name="modulus">BigNumber modulus</param>
        /// <returns>BigNumber multiplier</returns>
        public static BigNumber Multiplier(BigNumber modulus)
        {
            BigNumber multiplier = GenerateRandomNumberInRange((modulus / 2) + 1, modulus - 1);
            while (GCD(modulus, multiplier) != 1)
            {
                multiplier = GenerateRandomNumberInRange((modulus / 2) + 1, modulus - 1);
            }

            return multiplier;
        }
        /// <summary>
        /// returns Greatest Common Divisor
        /// </summary>
        /// <param name="a">First BigNumber</param>
        /// <param name="b">Second BigNumber</param>
        private static BigNumber GCD(BigNumber a, BigNumber b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        /// <summary>
        /// generates BigNumber value in given range. If bit length is too small, it adds one bit every 100 000 cycles
        /// to avoid not having enough bits to have big enough number
        /// </summary>
        /// <param name="a1">lower value in range INCLUDED in range</param>
        /// <param name="a2">higher value in range INCLUDED in range</param>
        /// <returns>Random BigNumber in range</returns>
        private static BigNumber GenerateRandomNumberInRange(BigNumber a1, BigNumber a2)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            BigNumber generatedRandom;
            int tries = 100000;
            do
            {
                byte[] temp = new byte[_singleNumberBitLength / 8];
                rng.GetBytes(temp);
                generatedRandom = new BigNumber(temp);
                tries--;
                if (tries == 0)
                {
                    tries = 100000;
                    _singleNumberBitLength++;
                }
            } while (generatedRandom < a1 || generatedRandom > a2);

            Console.Out.WriteLine(_singleNumberBitLength);
            return generatedRandom;
        }
        /// <summary>
        /// Sums given supersequence
        /// </summary>
        /// <param name="supersequence"></param>
        /// <returns></returns>
        public static BigNumber SumSupersequence(List<BigNumber> supersequence)
        {
            BigNumber temp = new BigNumber(0);
            foreach (var bigNumber in supersequence)
            {
                temp += bigNumber;
                
            }

            return temp;
        }

    }

}