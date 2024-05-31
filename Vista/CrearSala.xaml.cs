using System.Windows;
using System.Windows.Controls;
using Chat.Modelo;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class CrearSala : UserControl
{
    private Sala _sala;

    public Usuario Usuario{ get; set; }
    public CrearSala() : this(new Sala()) { }

    public CrearSala(Sala sala)
    {
        InitializeComponent();
        _sala = sala;
        DataContext = _sala;
    }

    public void GuardarSala(object sender, RoutedEventArgs e)
    {
        ((Parent as FrameworkElement).DataContext as MainWindowVM).VistaActual = new SalaDeChat (Usuario) { Sala = _sala };



    }
    public void CancelarSala(object sender, RoutedEventArgs e)
    {
        ((Parent as FrameworkElement).DataContext as MainWindowVM).VistaActual = new ListaDeSalas{Usuario=Usuario};
        //var ListaDeSalas = new ListaDeSalas();
        //ListaDeSalas.Usuario = Usuario;
    }
}