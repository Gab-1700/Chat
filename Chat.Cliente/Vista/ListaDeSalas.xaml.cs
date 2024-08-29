using System.Windows.Controls;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class ListaDeSalas : UserControl
{
    public ListaDeSalasVM listaDeSalasVM{get; set; }
    public ListaDeSalas()
    {
        InitializeComponent();
    }
}