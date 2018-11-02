using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace RSA_Model
{
    public class Signature
    {
        public BigInteger BlindingFactor { get; private set; }
        private string _hashMessage;

        public List<BigInteger> CreateSignature(List<BigInteger> blindedMessage, (BigInteger, BigInteger) privateKey)
        {
            List<BigInteger> sign = new List<BigInteger>();
            foreach (BigInteger bigInteger in blindedMessage)
            {
                sign.Add(BigInteger.ModPow(bigInteger, privateKey.Item2, privateKey.Item1));
            }

            return sign;
        }

        public bool VerifySignature(string message, List<BigInteger> blindedSign, (BigInteger, BigInteger) publicKey)
        {
            List<BigInteger> sign = new List<BigInteger>();
            foreach (BigInteger bigInteger in blindedSign)
            {
                sign.Add(bigInteger * Keys.ModularInverse(BlindingFactor, publicKey.Item1) % publicKey.Item1);
            }

            int i = 0;
            foreach (char c in _hashMessage)
            {
                if (BigInteger.ModPow(sign[i], publicKey.Item2, publicKey.Item1) != c)
                    return false;
                i++;

            }

            return true;
        }     
    }
}
