using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Knapsack_View.Converters
{
    [ValueConversion(typeof(BigNumber), typeof(string))]
    public class BigNumberToStringConverter : IValueConverter
    {
        public static BigNumberToStringConverter Instance = new BigNumberToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            if ( (BigNumber)value == 0) return "";
            return ((BigNumber) value).ToHexString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new BigNumber(0);
            }
            Regex regex = new Regex("^[0-9A-Fa-f]+$");
            if (!regex.IsMatch((string)value))
            {
                return new BigNumber(0); ;
            }
            return new BigNumber((string)value,16);
        }
    }
}