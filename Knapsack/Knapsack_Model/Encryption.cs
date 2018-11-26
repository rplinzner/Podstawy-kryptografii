using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Knapsack_Model
{
    public class Encryption : ICrypto
    {
        #region properties
        public List<BigNumber> PublicKey { private get; set; }
        public List<BigNumber> PrivateKey { private get; set; }
        public BigNumber Multiplier { private get; set; }
        public BigNumber Modulus { private get; set; }
        #endregion
        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="publicKey"></param>
        public Encryption()
        {
            Multiplier = 0;
            Modulus = 0;
        }

        #endregion

        /// <summary>
        /// Method that encodes message using public key. Used by anyone.
        /// </summary>
        /// <param name="message">Message to encrypt</param>
        /// <exception cref="NullReferenceException"> Thrown when no public key was set</exception>
        /// <returns></returns>
        public string Encrypt(string message)
        {

            if (PublicKey == null) throw new NullReferenceException("Public key needs to be set for this operation");
            var charTab = message.ToCharArray();
            List<BigNumber> encodedChars = new List<BigNumber>(charTab.Length);
            for (int i = 0; i < charTab.Length; i++)
            {
                encodedChars.Add(GetCodedChar(charTab[i]));
            }
            return StringHelper.ConvertBigNumberListToString(encodedChars);
        }
        public string Decrypt(string message)
        {
            #region Check if properties are present
            if (Multiplier == 0) throw new NullReferenceException("Multiplier needs to be set for this operation");
            if (Modulus == 0) throw new NullReferenceException("Modulus needs to be set for this operation");
            if (PrivateKey == null) throw new NullReferenceException("Private Key needs to be set for this operation");
            #endregion

            List<BigNumber> decodedBN = DecodeBigNumbers(message);
            List<BigNumber> privateKeyCopy = new List<BigNumber>(PrivateKey);
            privateKeyCopy.Reverse();
            string decodedMessage = "";
            foreach (var t in decodedBN)
            {
                BigNumber rest = t;
                var charBitArray = new BitArray(8);
                for (int j = 0; j < privateKeyCopy.Count; j++)
                {
                    if (rest >= privateKeyCopy[j])
                    {
                        charBitArray[j] = true;
                        rest = rest - privateKeyCopy[j];
                    }
                    else charBitArray[j] = false;
                }
                var singleCharInByte = ConvertToByte(charBitArray);
                decodedMessage += Encoding.UTF8.GetString(singleCharInByte);
            }
            return decodedMessage;
        }
        #region helper methods
        private List<BigNumber> DecodeBigNumbers(string message)
        {
            BigNumber multiplyFactor = Multiplier.ModInverse(Modulus); //does multiplier^(-1)modModulus
            List<BigNumber> charsEncryptedBN = StringHelper.DecodeStringToBNList(message); //convert numbers from string to BN array
            List<BigNumber> decodedEncryptedBNChars = new List<BigNumber>(charsEncryptedBN.Count);

            for (int i = 0; i < charsEncryptedBN.Count; i++)
            {
                BigNumber BNDecode = new BigNumber((charsEncryptedBN[i] * multiplyFactor) % Modulus);
                decodedEncryptedBNChars.Add(BNDecode);
            }
            return decodedEncryptedBNChars;
        }
        private byte[] ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes;
        }
        private BigNumber GetCodedChar(char c)
        {
            BigNumber temp = new BigNumber(0);
            char[] tempChar = { c };
            byte[] charByte = System.Text.Encoding.UTF8.GetBytes(tempChar);
            BitArray charBitArray = new BitArray(charByte);
            List<BigNumber> reversedPublicKey = new List<BigNumber>(PublicKey);
            reversedPublicKey.Reverse();
            for (int i = 0; i < PublicKey.Count; i++)
            {
                if (charBitArray[i]) temp += reversedPublicKey[i];
            }
            return temp;
        }

        #endregion

    }
}
