using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RSA_Model;

namespace RSA_ModelTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Keys keys = new Keys();
            keys.GenerateKeys();
            Signature sig = new Signature();
            List<BigInteger> blindedMessage = sig.BlindMessage("test", keys.PublicKey);
            List<BigInteger> blindedSignature = sig.CreateSignature(blindedMessage, keys.PrivateKey);

            Console.WriteLine(sig.VerifySignature("test", blindedSignature, keys.PublicKey));

        }
    }
}