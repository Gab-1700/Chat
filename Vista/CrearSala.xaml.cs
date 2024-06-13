using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chat.Modelo;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class CrearSala : UserControl
{
    public Sala Sala { get; set; }

    public Usuario Usuario{ get; set; }

    public ICommand NavegacionARegistroDeUsuario { get; set; }
    public ICommand NavegacionAListaDeSalas { get; set; }
    public ICommand NavegacionASalaDeChat { get; set; }
    
    public CrearSala()
    {

        InitializeComponent();
        Sala = new Sala();
        DataContext = this;
    }
}