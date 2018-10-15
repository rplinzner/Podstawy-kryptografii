using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESX_Model
{
    public class DesHelper
    {
        public static List<BitArray> StringToBitArrayBlocks(string text, Encoding encoding)
        {

            byte[] byteText;
            byteText = encoding.GetBytes(text);

            int numberOfBits = byteText.Length * 8;
            if (numberOfBits % 64 != 0)
                byteText = FillTo64Bits(byteText);
            numberOfBits = byteText.Length * 8;
            int numberOfBlocks = (int)(numberOfBits / 64.0);
            List<BitArray> blocks = new List<BitArray>(numberOfBlocks);
            for (int i = 0; i < numberOfBlocks; i++)
            {
                blocks.Add(new BitArray(byteText.ToList().GetRange(i * 8, 8).ToArray()));
            }

            return blocks;


        }
        public static List<BitArray> BinaryStringToBitArrayBlocks(string text)
        {


            int numberOfBits = text.Length;
            int numberOfBlocks = (int)(numberOfBits / 64.0);
            List<BitArray> blocks = new List<BitArray>(numberOfBlocks);
            for (int i = 0; i < numberOfBlocks; i++)
            {
                blocks.Add(new BitArray(text.Skip(i * 64).Take(64).Select(t => t != '0').ToArray()));
            }

            return blocks;
        }

        public static byte[] FillTo64Bits(byte[] byteText)
        {

            byte[] filled = new byte[byteText.Length + 8 - byteText.Length % 8];
            for (int i = 0; i < byteText.Length; i++)
            {
                filled[i] = byteText[i];
            }
            for (int i = byteText.Length; i < filled.Length; i++)
            {
                filled[i] = 0;
            }

            return filled;
        }

        public static void DivideToTwoArrays(BitArray orignal, out BitArray leftArray, out BitArray rightArray)
        {
            int size = orignal.Length / 2;
            leftArray = new BitArray(size);
            rightArray = new BitArray(size);

            for (int i = 0; i < size; i++)
            {
                leftArray[i] = orignal[i];
                rightArray[i] = orignal[i + size];
            }

        }

        public static void ConnectTwoArraysIntoOne(BitArray leftArray, BitArray rightArray, out BitArray connectedArray)
        {
            connectedArray = new BitArray(leftArray.Length + rightArray.Length);
            for (int i = 0; i < leftArray.Length; i++)
            {
                connectedArray[i] = leftArray[i];
            }
            for (int i = 0; i < rightArray.Length; i++)
            {
                connectedArray[i + leftArray.Length] = leftArray[i];
            }
        }


        public static BitArray ShiftLeft(BitArray array)
        {
            BitArray shiftedArray = new BitArray(array.Count);
            for (int i = 1; i < array.Count; i++)
            {
                shiftedArray[i - 1] = array[i];
            }

            shiftedArray[array.Count - 1] = array[0];
            return shiftedArray;
        }

        public static BitArray BinaryStringTo64BitArray(string text)
        {
            BitArray binaryArray = new BitArray(64);
            for (int i = 0; i < 64; i++)
            {
                binaryArray[i] = text[i] != '0';
            }
            return new BitArray(binaryArray);
        }
    }
}
