using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chat.Modelo;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class ListaDeSalas : UserControl
{
    public Usuario Usuario { get; set; }
    public ICommand NavegacionACrearSala { get; set; }
    public ICommand NavegacionARegistroDeUsuario { get; set; }
    public ListaDeSalas()
    {
        InitializeComponent();
        var salas = new List<Sala>();

        Salas.ItemsSource = salas;
        DataContext = this;

    }

}