using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Windows.Data;

namespace RSA_View.Converters
{
    [ValueConversion(typeof(List<RSABigInteger>), typeof(string))]
    public class BigIntegerListToStringConverter : IValueConverter
    {
        public static BigIntegerListToStringConverter Instance = new BigIntegerListToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string list = String.Empty;
            if (value != null)
            {
                foreach (RSABigInteger bigInteger in (List<RSABigInteger>)value)
                {
                    list += bigInteger.ToString() + ',';
                }

                list = list.Remove(list.Length - 1);
                return list;
            }

            return String.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<RSABigInteger> list = new List<RSABigInteger>();
            var lines = ((string)value).Split(',');
            foreach (string line in lines)
            {
                list.Add(new RSABigInteger(line,10));
            }

            return list;
        }
    }
}
