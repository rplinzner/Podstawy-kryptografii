using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_Model
{
    public static class IntExtensionMethods
    {
        /// <summary>
        /// Extension Method for Integer. Calculates Modular Inversion. Need to be used as BigInteger usage is forbidden
        /// </summary>
        /// <param name="a">integer</param>
        /// <param name="m">modulus</param>
        /// <returns>Modular multiplicative inverse</returns>
        /// <exception cref="DivideByZeroException">This exception is thrown when there is no Modular Inversion</exception>
        public static int ModInverse(this int a, int m)
        {
            if (m == 1) return 0;
            int m0 = m;
            (int x, int y) = (1, 0);

            while (a > 1)
            {
                int q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }
    }
}
