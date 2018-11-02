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

        }
        [TestMethod]
        public void TestStringToBitArrayBlocksMethod()
        {
            string key = "0011000100110010001100110011010000110101001101100011011100111000";
            string key1 = "1011000100110010101100110011010000111101101101101001011100111000";
            string key2 = "0101000100011010001100110101010000110101001101101011011100111000";
            Encryptor enc = new Encryptor(key, key1, key2);
            Decryptor dec = new Decryptor(key, key2, key1);
            Console.WriteLine(dec.Decrypt(enc.Encrypt("1234567898543")));

        }
    }
}

