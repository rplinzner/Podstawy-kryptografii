using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Model
{
    public class Encoding
    {
        private int[] _publicKey;

        #region constructor
        
        public Encoding() //TODO Add field initialization
        {
            
        }
#endregion

        public Encoding(int[] publicKey)
        {
            _publicKey = publicKey;
        }

        public int[] Encode(string message)
        {
            var charTab = message.ToCharArray();
            int[] encodedChars = new int[charTab.Length];
            for (int i = 0; i < charTab.Length; i++)
            {
                encodedChars[i] = GetCodedChar(charTab[i]); 
            }
            return encodedChars;
        }
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
    }
}
