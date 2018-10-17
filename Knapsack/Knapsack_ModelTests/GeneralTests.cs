using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Knapsack_Model;

namespace Knapsack_ModelTests
{
    [TestClass]
    public class GeneralTests
    {
        [TestMethod]
        public void GeneratingPublicKeyTestPermute()
        {
            var publicKey = new PublicKeyGenerator(DefaultValues.PrivateKey(), DefaultValues.Modulus(),
                DefaultValues.Multiplier(), DefaultValues.Permutation());
           
            foreach (var i in publicKey.GeneratePublicKey())
            {
                Console.Out.WriteLine(i);
            }

            
            Console.Out.WriteLine("After permutation:");
            foreach (var i in publicKey.GetPublicKey())
            {
                Console.Out.WriteLine(i);
            }

            Console.Out.WriteLine("encoding");
            Encryption enc = new Encryption(publicKey.GetPublicKey());
            var temp = enc.Encrypt("luls");
            foreach (var i in temp)
            {
                Console.Out.WriteLine(i);
            }
            Assert.AreEqual(temp[1], 3736);

        }
    }
}
