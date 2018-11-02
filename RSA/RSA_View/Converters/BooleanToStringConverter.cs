using System;
using System.Globalization;
using System.Windows.Data;

namespace RSA_View.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BooleanToStringConverter : IValueConverter
    {
        public static BooleanToStringConverter Instance = new BooleanToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                if ((string)parameter == "Blind")
                {
                    return "Message is generated and blinded!";
                }
                return "Keys are generated!";
            }

            if ((string)parameter == "Blind")
            {
                return "Message is not generated or not blinded!";
            }

            return "Keys are not generated!";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
