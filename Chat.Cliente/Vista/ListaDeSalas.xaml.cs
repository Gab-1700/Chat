using System.Windows.Controls;
using Chat.Cliente.VistaModelo;

namespace Chat.Vista;
public partial class ListaDeSalas : UserControl
{
    public ListaDeSalas(ListaDeSalasVM listaDeSalasVM)
    {
        InitializeComponent();
        DataContext = listaDeSalasVM;
    }
}