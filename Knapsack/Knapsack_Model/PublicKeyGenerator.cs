using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Model
{
    /// <summary>
    /// Contains methods needed for generating public key
    /// </summary>
    public class PublicKeyGenerator
    {
        #region fields
        private readonly int[] _privateKey;
        private readonly int _modulus;
        private readonly int _multiplier;
        private readonly byte[] _permutation;
#endregion
        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="privateKey">Private Key</param>
        /// <param name="modulus">Modulus</param>
        /// <param name="multiplier">Multiplier</param>
        /// <param name="permutation">PermutationTable used to permute initially generated key</param>
        public PublicKeyGenerator(int[] privateKey, int modulus, int multiplier, byte[] permutation)
        {
            _privateKey = privateKey;
            _modulus = modulus;
            _multiplier = multiplier;
            _permutation = permutation;
        }
#endregion

        /// <summary>
        /// Generates initial public key, without additional permutation
        /// </summary>
        /// <returns>Weak public key</returns>
#if DEBUG
        public int[] GeneratePublicKey()
#else
        private int[] GeneratePublicKey()
#endif
        {
            var publicKey = new int[8];

            for (int i = 0; i < _privateKey.Length; i++)
            {
                publicKey[i] = (_privateKey[i] * _multiplier) % _modulus;
            }
            return publicKey;
        }
        /// <summary>
        /// Generates final form of public key. Includes permutation
        /// </summary>
        /// <returns>Public Key after permutation</returns>
        public int[] GetPublicKey()
        {
            var publicKey = Permutation.Permute(_permutation, GeneratePublicKey());
            return GeneratePublicKey();
        }

    }
}
