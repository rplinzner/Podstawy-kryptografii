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
                DefaultValues.Multiplier(), DefaultValues.PermutationTable());
           
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
            Encryption enc = new Encryption();
            enc.PublicKey = publicKey.GetPublicKey();
            enc.PrivateKey = DefaultValues.PrivateKey();
            enc.PermutationTable = DefaultValues.PermutationTable();
            enc.Modulus = DefaultValues.Modulus();
            enc.Multiplier = DefaultValues.Multiplier();
            var encryptedMessage = enc.Encrypt("ala ma kota a kot na ale");
            Console.Out.WriteLine(encryptedMessage);
            Console.Out.WriteLine("Decoding");
            var decrypted = enc.Decrypt(encryptedMessage);
            Console.Out.WriteLine(decrypted);



        }
    }
}
