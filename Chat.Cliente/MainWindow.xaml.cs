using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Chat.Modelo;
using Chat.Vista;
using Chat.VistaModelo;

namespace Chat;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();  
        DataContext = new MainWindowVM();   
        
    }
}