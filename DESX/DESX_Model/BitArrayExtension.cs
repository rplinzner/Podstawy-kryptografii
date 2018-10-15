using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESX_Model
{
    public static class BitArrayExtension
    {
        public static BitArray GetRange(this BitArray array, int startIndex, int count)
        {
            BitArray newBitArray= new BitArray(count);
            for (int i = 0; i < count; i++)
            {
                newBitArray[i] = array[i + startIndex];
            }

            return newBitArray;
        }

        public static BitArray Add(this BitArray array, BitArray arrayToAdd)
        {
            BitArray newBitArray = new BitArray(array.Count+arrayToAdd.Count);
            for (int i = 0; i < array.Count; i++)
            {
                newBitArray[i] = array[i];
            }

            for (int i = 0; i < arrayToAdd.Count; i++)
            {
                newBitArray[i + array.Count] = arrayToAdd[i];
            }

            return newBitArray;
        }

        public static int ToInt(this BitArray array)
        {
            int[] integer= new int[1];
            array.CopyTo(integer, 0);
            return integer[0];
        }
        public static string TToString(this BitArray array)
        {
            string text = null;
            for (int i = 0; i < array.Length/8; i++)
            {
                char temp = (char)array.GetRange(i*8,8).ToInt();
                text += temp;
            }

            return text;
        }

    }
}
