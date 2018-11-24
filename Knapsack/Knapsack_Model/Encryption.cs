using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Model
{
    public class Encryption : ICrypto
    {
        #region properties
        public int[] PublicKey { private get; set; }
        public int[] PrivateKey { private get; set; }
        public byte[] PermutationTable { private get; set; }
        public int Multiplier { private get; set; }
        public int Modulus { private get; set; }
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
        /// <returns></returns>
        public string Encrypt(string message)
        {

            if(PublicKey==null) throw new NullReferenceException("Public key needs to be set for this operation");
            var charTab = message.ToCharArray();
            int[] encodedChars = new int[charTab.Length];
            for (int i = 0; i < charTab.Length; i++)
            {
                encodedChars[i] = GetCodedChar(charTab[i]); 
            }
            return StringHelper.ConvertIntTableToString(encodedChars);
        }
        public string Decrypt(string message)
        {
            #region Check if properties are present
            if(PermutationTable == null) throw new NullReferenceException("PermutationTable table needs to be set for this operation");
            if(Multiplier == 0) throw new NullReferenceException("Multiplier needs to be set for this operation");
            if(Modulus == 0) throw new NullReferenceException("Modulus needs to be set for this operation");
            if(PrivateKey == null) throw new NullReferenceException("Private Key needs to be set for this operation");
#endregion

            int[] decodedInts = DecodeInts(message);
            int[] privateKeyCopy = new int[PrivateKey.Length];
            Array.Copy(PrivateKey, privateKeyCopy, PrivateKey.Length);
            Array.Reverse(privateKeyCopy);
            string decodedMessage ="";
            foreach (var t in decodedInts)
            {
                int rest = t;
                var charBitArray = new BitArray(8);
                for (int j = 0; j < privateKeyCopy.Length; j++)
                {
                    if (rest >= privateKeyCopy[j])
                    {
                        charBitArray[j] = true;
                        rest = rest - privateKeyCopy[j];
                    }
                    else charBitArray[j] = false;
                }
                //BitArray permutedCharBitArray = Knapsack_Model.Permutation.PermuteBitArray(PermutationTable, charBitArray);
                //var SingleCharInByte = ConvertToByte(permutedCharBitArray);
                var singleCharInByte = ConvertToByte(charBitArray);
                decodedMessage += Encoding.UTF8.GetString(singleCharInByte);
            }
            return decodedMessage;
        }
        #region helper methods
        //TODO Add summary + comments
        private int[] DecodeInts(string message)
        {
            //int multiplyFactor = Multiplier.ModInverse(Modulus); //does multiplier^(-1)mod900
            int[] charsEncryptedInt = StringHelper.DecodeString(message); //convert numbers from string to int array
            int[] decodedEncryptedIntChars = new int[charsEncryptedInt.Length];

            for (int i = 0; i < charsEncryptedInt.Length; i++)
            {
                //int intDecode = (charsEncryptedInt[i] * multiplyFactor) % Modulus;
                //decodedEncryptedIntChars[i] = intDecode;
            }
            return decodedEncryptedIntChars;
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
        private int GetCodedChar(char c)
        {
            int temp = 0;
            char[] tempChar = {c};
            byte[] charByte = System.Text.Encoding.UTF8.GetBytes(tempChar);
            BitArray charBitArray = new BitArray(charByte);
            int[] reversedPublicKey = new int[PublicKey.Length];
            Array.Copy(PublicKey, reversedPublicKey, PublicKey.Length);
            Array.Reverse(reversedPublicKey);
            for (int i = 0; i < PublicKey.Length; i++)
            {
                if (charBitArray[i]) temp += reversedPublicKey[i];
            }
            return temp;
        }
         
#endregion
        
    }
}
