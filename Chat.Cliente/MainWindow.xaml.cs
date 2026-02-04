using System.Windows;
using Chat.Cliente.VistaModelo;

namespace Chat.Cliente;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{

    public MainWindow(MainWindowVM mainWindowVM)
    {
        InitializeComponent();  
        DataContext = mainWindowVM;   
    }
}