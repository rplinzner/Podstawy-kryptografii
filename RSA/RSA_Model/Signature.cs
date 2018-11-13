using System.Collections.Generic;
using System.Text;

namespace RSA_Model
{
    public class Signature
    {
        public RSABigInteger BlindingFactor { get; private set; }
        private string _message;

        public List<RSABigInteger> CreateSignature(List<RSABigInteger> blindedMessage, (RSABigInteger, RSABigInteger) privateKey)
        {
            List<RSABigInteger> sign = new List<RSABigInteger>();
            foreach (RSABigInteger bigInteger in blindedMessage)
            {
                sign.Add(bigInteger.modPow(privateKey.Item2, privateKey.Item1));
            }

            return sign;
        }

        public bool VerifySignature(string message, List<RSABigInteger> blindedSign, (RSABigInteger, RSABigInteger) publicKey)
        {
            List<RSABigInteger> sign = new List<RSABigInteger>();
            foreach (RSABigInteger bigInteger in blindedSign)
            {
                sign.Add(bigInteger * Keys.ModularInverse(BlindingFactor, publicKey.Item1) % publicKey.Item1);
            }

            int i = 0;
            foreach (char c in _message)
            {
                if (sign[i].modPow(publicKey.Item2, publicKey.Item1) != (RSABigInteger)(int)c)
                    return false;
                i++;

            }

            return true;
        }

        public List<RSABigInteger> BlindMessage(string message, (RSABigInteger, RSABigInteger) publicKey)
        {
            do
            {
                BlindingFactor = Keys.GenerateNumber(publicKey.Item1);
            } while (publicKey.Item1.gcd(BlindingFactor) != 1);
            
            byte[] bytes = Encoding.UTF8.GetBytes(message);
           
            _message = string.Empty;
            foreach (byte x in bytes)
            {
                _message += x.ToString();
            }
           
            List<RSABigInteger> blindedMessage = new List<RSABigInteger>();
            foreach (char c in _message)
            {

                blindedMessage.Add((RSABigInteger)(int)c * BlindingFactor.modPow(publicKey.Item2, publicKey.Item1));
            }

            return blindedMessage;
        }
    }
}
