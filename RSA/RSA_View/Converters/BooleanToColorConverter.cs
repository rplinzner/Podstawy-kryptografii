using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RSA_View.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BooleanToColorConverter : IValueConverter
    {
        public static BooleanToColorConverter Instance = new BooleanToColorConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return new SolidColorBrush(Colors.LawnGreen);
            }
            else
            {
                return new SolidColorBrush(Colors.Red);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
