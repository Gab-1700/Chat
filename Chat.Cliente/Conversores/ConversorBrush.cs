using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Cliente.Conversores;
public class ConversorBrush : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string colorStr)
        {
            var color = (Color)ColorConverter.ConvertFromString(colorStr);
            return new SolidColorBrush(color);
        }
        else
        {
            return Brushes.Black;
        }
        
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            return brush.Color.ToString();
        }
        else
        {
            return "#000000";
        }
    }
}