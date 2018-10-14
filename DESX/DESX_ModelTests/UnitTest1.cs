using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DESX_Model;

namespace DESX_ModelTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var input = new InputHandler("cos tam sobie napisałem", Encoding.UTF8);
            Console.Out.WriteLine(input.PlainTextInBytes.Length);
            foreach (var inputPlainTextInByte in input.PlainTextInBytes)
            {
                Console.Out.WriteLine(inputPlainTextInByte);
            }

            Console.Out.WriteLine(input.PlainTextInBytes.Last());
        }
        [TestMethod]
        public void TestPermuteMethod()
        {
            byte[] permutation = new byte[]
            {
                10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60,
                61, 62, 63
            };
            byte[] byteArray = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};
            //byte Array in bits:
            // 10000000 - 1
            // 01000000 - 2
            // 11000000 - 3
            // 00100000 - 4
            // 10100000 - 5
            // 01100000 - 6
            // 11100000 - 7
            // 00010000 - 8

            byte[] afterPermutation=Permutations.Permute(permutation, byteArray);
            //after permutation should be:
            // 01000000 - 2
            // 00100000 - 4
            // 11000000 - 3
            // 00100000 - 4 
            // 10100000 - 5
            // 01100000 - 6
            // 11100000 - 7
            // 00010000 - 8


            foreach (byte b in afterPermutation)
            {
                Console.WriteLine(b);
                //TODO: Do tests for Permute Method
            }

        }
    }
}
