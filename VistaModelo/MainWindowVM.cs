using System.ComponentModel;
using System.Windows.Controls;
using Chat.Vista;

namespace Chat.VistaModelo;

public class MainWindowVM : INotifyPropertyChanged
{
    private UserControl _vistaActual;
    public UserControl VistaActual
    {
        get { return _vistaActual; }
        set
        {
            _vistaActual = value;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("VistaActual"));
        }

    }

    public MainWindowVM()
    {
        _vistaActual = new RegistroDeUsuario();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

}