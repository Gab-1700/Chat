using System.Windows;
using System.Windows.Input;
using Chat.Cliente.Comandos;
using Chat.Cliente.VistaModelo;
using Chat.Vista;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Chat.Cliente;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs args)
    {
        base.OnStartup(args);

        var servicios = new ServiceCollection();

        servicios.AddSingleton<MainWindowVM>();
        servicios.AddSingleton<RegistroDeUsuarioVM>();
        servicios.AddSingleton<ListaDeSalasVM>();
        servicios.AddSingleton<CrearSalaVM>();
        servicios.AddSingleton<SalaDeChatVM>();

        servicios.AddSingleton<MainWindow>();
        servicios.AddSingleton<RegistroDeUsuario>();
        servicios.AddSingleton<ListaDeSalas>();
        servicios.AddSingleton<CrearSala>();
        servicios.AddSingleton<SalaDeChat>();

        servicios.AddSingleton<AbrirRegistroDeUsuario>();
        servicios.AddSingleton<AbrirListaDeSalas>();
        servicios.AddSingleton<AbrirSalaDeChat>();
        servicios.AddSingleton<AbrirCrearSala>();

        servicios.AddSingleton(new HubConnectionBuilder()
                 .WithUrl("http://localhost:5037/Chat")
                 .Build());

        var proveedor = servicios.BuildServiceProvider();
        var mainWindow = proveedor.GetRequiredService<MainWindow>();        //dame la vista
        mainWindow.Show();                                                  // mostrar
    }
}

