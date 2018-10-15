﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESX_Model
{
    public class DesHelper
    {
        public static List<BitArray> StringToBitArrayBlocks(string text)
        {
            byte[] byteText;
            byteText = Encoding.UTF8.GetBytes(text);

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

        public static byte[] FillTo64Bits(byte[] byteText)
        {

            byte[] filled = new byte[byteText.Length + byteText.Length % 8];
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
                rightArray[i + size] = orignal[i + size];
            }
            
        }

        public static BitArray ShiftLeft(BitArray array)
        {
            BitArray shiftedArray=new BitArray(array.Count);
            for (int i = 1; i < array.Count; i++)
            {
                shiftedArray[i - 1] = array[i];
            }

            shiftedArray[array.Count - 1] = array[0];
            return shiftedArray;
        }
    }
}