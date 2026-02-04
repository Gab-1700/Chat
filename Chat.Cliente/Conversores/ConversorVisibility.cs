using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Chat.Cliente.Conversores;

public class ConversorVisibility : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool esAnfitrion)
        {
            return esAnfitrion ? Visibility.Visible : Visibility.Collapsed;
        }
        else
        {
            return Visibility.Collapsed;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Visible;
        }
        else
        {
            return false;
        }
    }
}