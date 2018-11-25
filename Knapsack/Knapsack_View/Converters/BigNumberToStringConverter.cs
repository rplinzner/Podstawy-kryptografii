using System;
using System.Globalization;
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
            return ((BigNumber) value).ToHexString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BigNumber((string)value,16);
        }
    }
}