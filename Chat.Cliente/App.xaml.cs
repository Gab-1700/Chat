using System.Net.Http;
using System.Windows;
using Chat.Cliente.VistaModelo;
using Chat.Services;
using Chat.Vista;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        servicios.AddLogging(configuracion =>
        {
            configuracion.ClearProviders();
            configuracion.AddDebug();
            configuracion.SetMinimumLevel(LogLevel.Debug);
        });

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

        servicios.AddSingleton<NavigationService>();

        servicios.AddSingleton(new HubConnectionBuilder()
                 .WithUrl("http://localhost:5037/hubs/chat")
                 .Build());
        servicios.AddSingleton(new HttpClient { BaseAddress = new Uri("http://localhost:5037/") });

        var proveedor = servicios.BuildServiceProvider();
        var mainWindow = proveedor.GetRequiredService<MainWindow>();        //dame la vista
        mainWindow.Show();                                                  // mostrar
    }
}

