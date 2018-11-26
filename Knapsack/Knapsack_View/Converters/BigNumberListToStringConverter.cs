using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Knapsack_View.Converters
{
    [ValueConversion(typeof(List<BigNumber>), typeof(string))]
    public class BigNumberListToStringConverter : IValueConverter
    {
        public static BigNumberListToStringConverter Instance = new BigNumberListToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            StringBuilder str = new StringBuilder();
            foreach (var i in (List<BigNumber>) value)
            {
                str.Append(i.ToHexString());
                str.Append('.');
            }
            str.Length--; //deletes last separator

            return str.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string) value))
            {
                return null;
            }
            Regex regex = new Regex("^[0-9A-Fa-f\\.]+$");
            if (!regex.IsMatch((string) value))
            {
                return null;
            }
            string[] elemEncrypted = ((string)value).Split('.');
            List<BigNumber> charDecodedBigNumbers = new List<BigNumber>(elemEncrypted.Length);
            for (int i = 0; i < elemEncrypted.Length; i++)
            {
                //parse from string, hex
                charDecodedBigNumbers.Add(new BigNumber(elemEncrypted[i], 16));
            }

            return charDecodedBigNumbers;
        }
    }
}