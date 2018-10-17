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
        //TODO Move fields to methods
        private readonly int[] _publicKey;

        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="publicKey"></param>
        public Encryption(int[] publicKey)
        {
            _publicKey = publicKey;
        }

        #endregion

        /// <summary>
        /// Method that encodes message using public key. Used by anyone.
        /// </summary>
        /// <param name="message">Message to encrypt</param>
        /// <returns></returns>
        public string Encrypt(string message)
        {
            var charTab = message.ToCharArray();
            int[] encodedChars = new int[charTab.Length];
            for (int i = 0; i < charTab.Length; i++)
            {
                encodedChars[i] = GetCodedChar(charTab[i]); 
            }

            return ConvertIntTableToString(encodedChars);
        }
        public int[] Decrypt(string message)
        {
            //TODO Change to separate method
            string[] CharsEncrypted = message.Split('.');
            int[] CharsEncryptedInt = new int[CharsEncrypted.Length];
            for (int i = 0; i < CharsEncrypted.Length; i++)
            {
                CharsEncryptedInt[i] = Int32.Parse(CharsEncrypted[i]);
            }

            return CharsEncryptedInt;
        }
        #region helper methods
        //TODO Add summary + comments
        private int GetCodedChar(char c)
        {
            int temp = 0;
            char[] tempChar = {c};
            byte[] charByte = System.Text.Encoding.UTF8.GetBytes(tempChar);
            BitArray charBitArray = new BitArray(charByte);
            int[] reversedPublicKey = new int[_publicKey.Length];
            Array.Copy(_publicKey, reversedPublicKey, _publicKey.Length);
            Array.Reverse(reversedPublicKey);
            for (int i = 0; i < _publicKey.Length; i++)
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
