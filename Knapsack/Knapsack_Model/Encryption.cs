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

            return ConvertIntTableToString(encodedChars);
        }
        public string Decrypt(string message)
        {
            if(PermutationTable == null) throw new NullReferenceException("PermutationTable table needs to be set for this operation");
            if(Multiplier == 0) throw new NullReferenceException("Multiplier needs to be set for this operation");
            if(Modulus == 0) throw new NullReferenceException("Modulus needs to be set for this operation");
            if(PrivateKey == null) throw new NullReferenceException("Private Key needs to be set for this operation");
            int multiplyFactor = Multiplier.ModInverse(Modulus); //does multiplier^(-1)mod900

            int[] CharsEncryptedInt = DecodeString(message); //convert numbers from string to int array
            int[] DecodedEncryptedIntChars = new int[CharsEncryptedInt.Length];
            
            for (int i = 0; i < CharsEncryptedInt.Length; i++)
            {
                int intDecode = (CharsEncryptedInt[i] * multiplyFactor) % Modulus;
                DecodedEncryptedIntChars[i] = intDecode;
            }
            int[] PrivateKeyCopy = new int[PrivateKey.Length];
            Array.Copy(PrivateKey, PrivateKeyCopy, PrivateKey.Length);
            Array.Reverse(PrivateKeyCopy);
            //char[] DecodedChars = new char[DecodedEncryptedIntChars.Length];
            string decodedMessage ="";
            for (int i = 0; i < DecodedEncryptedIntChars.Length; i++)
            {
                int rest = DecodedEncryptedIntChars[i];
                BitArray charBitArray = new BitArray(8);
                for (int j = 0; j < PrivateKeyCopy.Length; j++)
                {
                    if (rest >= PrivateKeyCopy[j])
                    {
                        charBitArray[j] = true;
                        rest = rest - PrivateKeyCopy[j];
                    }
                    else charBitArray[j] = false;
                }

                //BitArray permutedCharBitArray = Knapsack_Model.Permutation.PermuteBitArray(PermutationTable, charBitArray);
                //var bycik = ConvertToByte(permutedCharBitArray);
                var bycik = ConvertToByte(charBitArray);
                decodedMessage += Encoding.UTF8.GetString(bycik);
            }

            return decodedMessage;

        }
        #region helper methods

        byte[] ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes;
        }

#if DEBUG
        public int[] DecodeString(string str)
        #else
        private int[] DecodeString(string str)
#endif
        {
            string[] CharsEncrypted = str.Split('.');
            int[] CharsdecodedInt = new int[CharsEncrypted.Length];
            for (int i = 0; i < CharsEncrypted.Length; i++)
            {
                CharsdecodedInt[i] = Int32.Parse(CharsEncrypted[i]);
            }

            return CharsdecodedInt;
        }

        //TODO Add summary + comments
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

        
        private string ConvertIntTableToString(int[] IntTable)
        {
            StringBuilder str = new StringBuilder();
            foreach (var i in IntTable)
            {
                str.Append(i);
                str.Append('.');
            }

            str.Length--; //deletes last separator

            return str.ToString();
        }
#endregion
        
    }
}
