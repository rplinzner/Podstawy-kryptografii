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
            string key = "00110001001100100011001100110100001101010011011000110111001110000011100100111000001101110011011000110111001110000011100100111000";  
            Encryptor enc = new Encryptor(key);
            Decryptor dec=new Decryptor(key);
            Console.WriteLine(dec.Decrypt(enc.Encrypt("PierdoloneKrypto")));
            
        }
    }
}

