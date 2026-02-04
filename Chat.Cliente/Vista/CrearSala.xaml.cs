using System.Windows.Controls;
using Chat.Cliente.VistaModelo;

namespace Chat.Vista;
public partial class CrearSala : UserControl
{
    public CrearSala(CrearSalaVM crearSalaVM)
    {
        InitializeComponent();
        DataContext = crearSalaVM;
    }
}