using System.Windows;
using System.Windows.Controls;
using Chat.Modelo;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class ListaDeSalas : UserControl
{
    public Usuario Usuario{ get; set;}
    public ListaDeSalas()
    {
        InitializeComponent();
        var salas = new List<Sala>();

        Salas.ItemsSource=salas;
        DataContext=this;
        
    }

    public void CrearSala(object sender, RoutedEventArgs e)
    {
        ((Parent as FrameworkElement).DataContext as MainWindowVM).VistaActual = new CrearSala{Usuario=Usuario};
    }

}