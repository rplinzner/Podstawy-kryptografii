using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Knapsack_Model;

namespace Knapsack_ModelTests
{
    [TestClass]
    public class GeneralTests
    {
        [TestMethod]
        public void GeneratingPublicKeyTestAndMessage()
        {
            var privateKey = Generator.PrivateKey();
            var modulus = Generator.Modulus(privateKey);
            var multiplier = Generator.Multiplier(modulus);
            var publicKey = new PublicKeyGenerator(privateKey, modulus,multiplier);
           
            foreach (var i in publicKey.GetPublicKey())
            {
                Console.Out.WriteLine(i);
            }

            Console.Out.WriteLine("encoding");
            Encryption enc = new Encryption();
            enc.PublicKey = publicKey.GetPublicKey();
            enc.PrivateKey = privateKey;
            enc.Modulus = modulus;
            enc.Multiplier = multiplier;
            var encryptedMessage = enc.Encrypt("Some Really hard sentence");
            Console.Out.WriteLine(encryptedMessage);
            Console.Out.WriteLine("Decoding");
            var decrypted = enc.Decrypt(encryptedMessage);
            Console.Out.WriteLine(decrypted);

          }

        [TestMethod]
        public void Generatingprivate()
        {
            List<BigNumber> key = Generator.PrivateKey();
            Console.Out.WriteLine("Key Generated");
            BigNumber mod = Generator.Modulus(key);
            Console.Out.WriteLine("Modulus done");
            BigNumber mul = Generator.Multiplier(mod);
            Console.Out.WriteLine("KEY");
            foreach (var bigNumber in key)
            {
                Console.Out.WriteLine(bigNumber.ToHexString());
            }

            Console.Out.WriteLine("MODULUS");
            Console.Out.WriteLine(mod.ToHexString());
            Console.Out.WriteLine("MULTIPLIER");
            Console.Out.WriteLine(mul.ToHexString());
            Console.Out.WriteLine("test if sequence is superincreasing");
            for (int i = 0; i < key.Count-1; i++)
            {
                Console.Out.WriteLine(key[i] < key[i+1] ? "YES" : "NO");
            }

            Console.Out.WriteLine("Test if modulus is greater then sum of super sequence");
            Console.Out.WriteLine(mod>Generator.SumSupersequence(key) ? "YES":"NO");
            
            
        }
    }
}
