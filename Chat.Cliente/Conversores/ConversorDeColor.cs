using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Conversores;

public class ConversorDeColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is SolidColorBrush brush)
        {
            return brush.Color;
        }
        else
        {
            return Colors.Black;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
       
         if (value is Color color)
        {
            return new SolidColorBrush(color);
        }
        else
        {
            return Brushes.Black;
        }

    }
}