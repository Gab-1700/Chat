using System.Windows.Controls;
using Chat.Cliente.VistaModelo;

namespace Chat.Vista;
public partial class SalaDeChat : UserControl
{
    public SalaDeChat(SalaDeChatVM salaDeChatVM)
    {
        InitializeComponent();
        DataContext = salaDeChatVM;
    }
}