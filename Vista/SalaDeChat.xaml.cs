using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Chat.Modelo;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class SalaDeChat : UserControl
{
    public ObservableCollection<Mensaje> Mensajes { get; set; }
    public ObservableCollection<Usuario> Usuarios { get; set; }
    public Usuario Usuario { get; set; }
    public Sala Sala { get; set; }
    public SalaDeChat(Usuario usuario)
    {
        InitializeComponent();

        Mensajes = new ObservableCollection<Mensaje>
        {
            new Mensaje() {Propietario= "Gabriel", Texto ="hola como estas", Fecha = DateTime.Now}
        };

        //Salas.ItemsSource=salas;
        //Chat
        Usuario=usuario;

        Usuarios = new ObservableCollection<Usuario> { Usuario  };
        //Usuarios = new ObservableCollection<Usuario>();
        //Usuarios.Add(Usuario);
        
        DataContext = this;
    }
    public void EnviarMensaje(object sender, RoutedEventArgs e)
    {
        var mensaje = new Mensaje { Texto = TxtMensaje.Text, Propietario = "Gab_1700", Fecha = DateTime.Now };
        Mensajes.Add(mensaje);
        TxtMensaje.Text = "";
    }

    public void CerrarSala(object sender, RoutedEventArgs e)
    {
        ((Parent as FrameworkElement).DataContext as MainWindowVM).VistaActual = new ListaDeSalas { Usuario = Usuario };
    }

    public void EditarSala(object sender, RoutedEventArgs e)
    {
        ((Parent as FrameworkElement).DataContext as MainWindowVM).VistaActual = new CrearSala(Sala) { Usuario = Usuario };
    }
}