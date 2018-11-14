using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RSA_Model
{
    public class Keys
    {
        private static int _size = 1024;
        private RSABigInteger _eNumber;
        private RSABigInteger _dNumber;
        private RSABigInteger _nNumber;
        public (RSABigInteger, RSABigInteger) PublicKey { get; private set; }
        public (RSABigInteger, RSABigInteger) PrivateKey { get; private set; }

        //generate random odd prime 1024-bit number
        public RSABigInteger GeneratePrimeNumber()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            RSABigInteger number;
            do
            {
                byte[] bytes = new byte[_size / 8];
                rng.GetBytes(bytes);
                RSABigInteger temp = new RSABigInteger(bytes);
                number = temp;
            } while (number.IsEven() || number <= 2 || !IsProbablyPrime(number));

            return number;
        }

        //generate random 1024-bit number (the r - factor in blind signature)
        public static RSABigInteger GenerateNumber(RSABigInteger value)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            RSABigInteger number;
            do
            {
                byte[] bytes = new byte[_size / 8];
                rng.GetBytes(bytes);
                RSABigInteger temp = new RSABigInteger(bytes);
                number = temp;
            } while (number <= 2 || number > value);

            return number;
        }

        //check the primality of a number
        public bool IsProbablyPrime(RSABigInteger value, int checkTries = 10)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            if (checkTries <= 0)
                checkTries = 10;

            RSABigInteger d = value - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            byte[] bytes = new byte[value.getBytes().LongLength];
            RSABigInteger a;

            for (int i = 0; i < checkTries; i++)
            {
                do
                {
                    rng.GetBytes(bytes);
                    a = new RSABigInteger(bytes);
                }
                while (a < 2 || a >= value - 2);

                RSABigInteger x = a.modPow(d, value);
                if (x == 1 || x == value - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = x.modPow(2, value);

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
        public List<RSABigInteger> GenerateTwoNumbers()
        {
            List<RSABigInteger> numbers = new List<RSABigInteger>();
            RSABigInteger a = GeneratePrimeNumber();
            numbers.Add(a);
            RSABigInteger b = GeneratePrimeNumber();
            while (b == a)
            {
                b = GeneratePrimeNumber();
            }
            numbers.Add(b);
            return numbers;
        }

        //designate the value of Euler function
        public RSABigInteger Euler(RSABigInteger p, RSABigInteger q)
        {
            return (p - 1) * (q - 1);
        }

        //designate the value of module
        public RSABigInteger Module(RSABigInteger p, RSABigInteger q)
        {
            return p * q;
        }

        //designate the value of modular inverse
        public static RSABigInteger ModularInverse(RSABigInteger a, RSABigInteger n)
        {
            RSABigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                RSABigInteger t = i / a, x = a;
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
            List<RSABigInteger> keys = GenerateTwoNumbers();
            
            RSABigInteger euler = Euler(keys[0], keys[1]);
            _nNumber = Module(keys[0], keys[1]);
            
            for (_eNumber = 3; _eNumber.gcd(euler) != 1; _eNumber += 2)
            {
            }

            _dNumber = ModularInverse(_eNumber, euler);
            
            PublicKey = (_nNumber, _eNumber);
            PrivateKey = (_nNumber, _dNumber);
        }    

    }
}
