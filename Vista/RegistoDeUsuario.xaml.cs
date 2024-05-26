
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Chat.Modelo;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class RegistroDeUsuario : UserControl
{

    public Usuario Usuario { get; set; }
    public RegistroDeUsuario()
    {
        InitializeComponent();
        Usuario = new Usuario(){
            Nombre="Pepe",Color=Colors.Blue
        };
        DataContext = Usuario;

    }

    public void GuardarUsuario(object sender, RoutedEventArgs e)
    {
            ((Parent as FrameworkElement).DataContext as MainWindowVM).VistaActual  = new SalaDeChat();
    }
}