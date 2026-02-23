using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chat.Cliente.Comandos;
using Chat.Vista;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Cliente.VistaModelo;
public class MainWindowVM : BaseVM<object>
{
    private UserControl? _vistaActual;
    public UserControl? VistaActual
    {
        get { return _vistaActual; }
        set
        {
            _vistaActual = value;
            OnPropertyChanged(nameof(VistaActual));
        }
    }

    public MainWindowVM(RegistroDeUsuario registroDeUsuario , HubConnection conexion )
    {
        VistaActual = registroDeUsuario;
        ConectarAlServidor(conexion);

    }
    

   private async void ConectarAlServidor(HubConnection conexion)
    {
         try
            {
                await conexion.StartAsync();                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
    }


}