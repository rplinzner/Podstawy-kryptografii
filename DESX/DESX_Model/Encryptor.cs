using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESX_Model
{
    public class Encryptor
    {
        private byte _blockSize = 8; //One block is 64bits=8bytes
        public List<BitArray> SubKeys { get; set; }
        public BitArray Key { get; set; }
        public BitArray KeyXor1 { get; set; }
        public BitArray KeyXor2 { get; set; }


        public Encryptor(string key,string keyXor1, string keyXor2)
        {
            SubKeys = new List<BitArray>();
            Key = DesHelper.BinaryStringTo64BitArray(key);
            KeyXor1 = DesHelper.BinaryStringTo64BitArray(keyXor1);
            KeyXor2 = DesHelper.BinaryStringTo64BitArray(keyXor2);
            Get16SubKeysFromKey();
        }

        public void Get16SubKeysFromKey()
        {
            BitArray permutatedKey = Permutations.Permute(Data.Permutation56Table, Key);
            DesHelper.DivideToTwoArrays(permutatedKey, out BitArray leftPermutatedKey, out BitArray rightPermutatedKey);
            List<BitArray> leftShitedKeys = new List<BitArray>(16);
            List<BitArray> rightShitedKeys = new List<BitArray>(16);
            leftShitedKeys.Add(DesHelper.ShiftLeft(leftPermutatedKey));
            rightShitedKeys.Add(DesHelper.ShiftLeft(rightPermutatedKey));
            for (int i = 0; i < 15; i++)
            {
                leftShitedKeys.Add(DesHelper.ShiftLeft(leftShitedKeys[i]));
                rightShitedKeys.Add(DesHelper.ShiftLeft(rightShitedKeys[i]));
            }

            List<BitArray> shiftedSubKeys = new List<BitArray>(16);
            for (int i = 0; i < 16; i++)
            {
                DesHelper.ConnectTwoArraysIntoOne(leftShitedKeys[i], rightShitedKeys[i], out BitArray connected);
                shiftedSubKeys.Add(connected);
                SubKeys.Add(Permutations.Permute(Data.Permutation48Table, shiftedSubKeys[i]));
            }

        }

        public string Encrypt(string text,Encoding encoding)
        {
            string encrypted=null;
            List<BitArray> blocks = DesHelper.StringToBitArrayBlocks(text,encoding);
            List<BitArray> xoredBlocks= new List<BitArray>();
            List<BitArray> permutatedBlocks = new List<BitArray>();
            foreach (BitArray bitArray in blocks)
            {
                xoredBlocks.Add(bitArray.Xor(KeyXor1));
            }
            foreach (BitArray bitArray in xoredBlocks)
            {
                permutatedBlocks.Add(Permutations.Permute(Data.InitialPermutationTable1, bitArray));
            }

            foreach (BitArray permutatedBlock in permutatedBlocks)
            {
                DesHelper.DivideToTwoArrays(permutatedBlock, out BitArray leftArray, out BitArray rightArray);

                for (int i = 0; i < 16; i++)
                {
                    BitArray temp = leftArray;
                    leftArray = rightArray;
                    rightArray = temp.Xor(FunctionF(rightArray, SubKeys[i]));
                }

                BitArray sum = leftArray.Add(rightArray);
                BitArray final = Permutations.Permute(Data.InitialPermutationTable2, sum);
                BitArray xoredFinal = final.Xor(KeyXor2);
                for (int i = 0; i < final.Length; i++)
                {
                    encrypted += xoredFinal[i] ? "1" : "0";
                }
            }

            return encrypted;



        }

        private BitArray FunctionF(BitArray rightArray, BitArray subKey)
        {
            BitArray xoredArray = subKey.Xor(FunctionE(rightArray));
            BitArray sboxedArray = FunctionSBox(xoredArray);
            return FunctionP(sboxedArray);
        }

        private BitArray FunctionE(BitArray rightArray)
        {
            return Permutations.Permute(Data.PetmutationETable, rightArray);
        }

        private BitArray FunctionSBox(BitArray array)
        {

            BitArray sBoxedArray=new BitArray(32);

            for (int i = 0; i < 8; i++)
            {
                BitArray temp = array.GetRange(6 * i, 6);
                sBoxedArray.Add(Permutations.Permute(Data.SBox[i], temp));
            }

            return sBoxedArray;
        }

        private BitArray FunctionP(BitArray array)
        {
            return Permutations.Permute(Data.PermutationPTable, array);
        }
    }
}
