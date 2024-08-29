using System.Windows.Controls;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class SalaDeChat : UserControl
{
    public SalaDeChatVM SalaDeChatVM { get; set;}
    public SalaDeChat()
    {
        InitializeComponent();
    }
}