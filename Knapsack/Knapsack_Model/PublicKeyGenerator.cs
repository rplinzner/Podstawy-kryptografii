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
        private readonly List<BigNumber> _privateKey;
        private readonly BigNumber _modulus;
        private readonly BigNumber _multiplier;
#endregion
        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="privateKey">Private Key</param>
        /// <param name="modulus">Modulus</param>
        /// <param name="multiplier">Multiplier</param>
        /// <param name="permutation">PermutationTable used to permute initially generated key</param>
        public PublicKeyGenerator(List<BigNumber> privateKey, BigNumber modulus, BigNumber multiplier)
        {
            _privateKey = privateKey;
            _modulus = modulus;
            _multiplier = multiplier;
        }
#endregion

        /// <summary>
        /// Generates initial public key, without additional permutation
        /// </summary>
        /// <returns>Weak public key</returns>
        private List<BigNumber> GeneratePublicKey()
        {
            var publicKey = new List<BigNumber>(_privateKey.Count);

            for (int i = 0; i < _privateKey.Count; i++)
            {
               publicKey.Add(new BigNumber((_privateKey[i] * _multiplier) % _modulus));
            }
            return publicKey;
        }
        /// <summary>
        /// Generates final form of public key. Includes permutation
        /// </summary>
        /// <returns>Public Key after permutation</returns>
        public List<BigNumber> GetPublicKey()
        {
            return GeneratePublicKey();
        }

    }
}
