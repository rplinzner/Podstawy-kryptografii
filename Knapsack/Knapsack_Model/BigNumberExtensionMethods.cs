using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Model
{
    public static class BigNumberExtensionMethods
    {
        /// <summary>
        /// Extension Method for BigNumber. Calculates Modular Inversion.
        /// </summary>
        /// <param name="a">value</param>
        /// <param name="m">modulus</param>
        /// <returns>Modular multiplicative inverse</returns>
        /// <exception cref="DivideByZeroException">This exception is thrown when there is no Modular Inversion</exception>
        public static BigNumber ModInverse(this BigNumber a, BigNumber m)
        {
            if (m == 1) return 0;
            BigNumber m0 = m;
            (BigNumber x, BigNumber y) = (1, 0);

            while (a > 1)
            {
                BigNumber q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }
    }
}
