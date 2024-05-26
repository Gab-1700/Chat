using System.Windows;
using Chat.Vista;
using Chat.VistaModelo;

namespace Chat;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{
    private MainWindowVM _mainWindowVM;
    public MainWindow()
    {
        InitializeComponent();
        _mainWindowVM = new MainWindowVM();
        DataContext = _mainWindowVM;
    }
}