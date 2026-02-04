using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Cliente.Conversores;

public class ConversorColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string colorStr)
        {
            return (Color)ColorConverter.ConvertFromString(colorStr);
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
            return color.ToString();
        }
        else
        {
            return "#000000";
        }
    }
}