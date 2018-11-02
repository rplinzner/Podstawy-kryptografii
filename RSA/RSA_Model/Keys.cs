using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Numerics;

namespace RSA_Model
{
    public class Keys
    {
        private static int _size = 1024;
        private BigInteger _eNumber;
        private BigInteger _dNumber;
        private BigInteger _nNumber;
        public (BigInteger, BigInteger) PublicKey { get; private set; }
        public (BigInteger, BigInteger) PrivateKey { get; private set; }

        //generate random odd prime 1024-bit number
        public BigInteger GeneratePrimeNumber()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            BigInteger number;
            do
            {
                byte[] bytes = new byte[_size / 8];
                rng.GetBytes(bytes);
                BigInteger temp = new BigInteger(bytes);
                number = temp;
            } while (number.IsEven || number <= 2 || !IsProbablyPrime(number));

            return number;
        }

        //generate random 1024-bit number (the r - factor in blind signature)
        public static BigInteger GenerateNumber(BigInteger value)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            BigInteger number;
            do
            {
                byte[] bytes = new byte[_size / 8];
                rng.GetBytes(bytes);
                BigInteger temp = new BigInteger(bytes);
                number = temp;
            } while (number <= 2 || number > value);

            return number;
        }

        //check the primality of a number
        public bool IsProbablyPrime(BigInteger value, int checkTries = 10)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            if (checkTries <= 0)
                checkTries = 10;

            BigInteger d = value - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            byte[] bytes = new byte[value.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < checkTries; i++)
            {
                do
                {
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= value - 2);

                BigInteger x = BigInteger.ModPow(a, d, value);
                if (x == 1 || x == value - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, value);

                    if (x == 1)
                        return false;

                    if (x == value - 1)
                        break;
                }

                if (x != value - 1)
                    return false;
            }

            return true;
        }

        //generate 2 random odd prime 1024-bit numbers
        public List<BigInteger> GenerateTwoNumbers()
        {
            List<BigInteger> numbers = new List<BigInteger>();
            BigInteger a = GeneratePrimeNumber();
            numbers.Add(a);
            BigInteger b = GeneratePrimeNumber();
            while (b == a)
            {
                b = GeneratePrimeNumber();
            }
            numbers.Add(b);
            return numbers;
        }

        //designate the value of Euler function
        public BigInteger Euler(BigInteger p, BigInteger q)
        {
            return (p - 1) * (q - 1);
        }

        //designate the value of module
        public BigInteger Module(BigInteger p, BigInteger q)
        {
            return p * q;
        }

        //designate the value of modular inverse
        public static BigInteger ModularInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }

        //establish values of dNumber and eNumber - designate the keys
        public void GenerateKeys()
        {
            List<BigInteger> keys = GenerateTwoNumbers();
            BigInteger euler = Euler(keys[0], keys[1]);
            _nNumber = Module(keys[0], keys[1]);

            for (_eNumber = 3; BigInteger.GreatestCommonDivisor(_eNumber, euler) != 1; _eNumber += 2)
            {
            }

            _dNumber = ModularInverse(_eNumber, euler);
            PublicKey = (_nNumber, _eNumber);
            PrivateKey = (_nNumber, _dNumber);
        }    

    }
}
