using System.Windows.Controls;
using Chat.Modelo;

namespace Chat.Vista;
public partial class ListaDeSalas : UserControl
{
    public ListaDeSalas()
    {
        InitializeComponent();
        var salas = new List<Sala>
        {
            new Sala() { Nombre = "Sala de Gerado", Capacidad = 10, Visibility = true },
            new Sala() { Nombre = "Sala de Ariel", Capacidad = 5, Visibility = false, Contraseña = "1234" }
        };
        Salas.ItemsSource=salas;

        
        
    }

}