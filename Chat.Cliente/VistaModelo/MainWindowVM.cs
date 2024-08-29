using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Chat.Modelo;
using Chat.Vista;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.VistaModelo;
public class MainWindowVM : BaseVM
{
    private UserControl _vistaActual;
    private UserControl _registroDeUsuario;
    private UserControl _listaDeSalas;
    private UserControl _salaDeChat;
    private UserControl _crearSala;
    private HubConnection _connection;

    public UserControl VistaActual
    {
        get { return _vistaActual; }
        set
        {
            _vistaActual = value;
            //PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(VistaActual)));
            OnPropertyChanged(nameof(VistaActual));
        }
    }

    //public void num() {Console.WriteLine(5);}
    //  ees equivalente a esto 
    //() => Console.WriteLine(5)
    // () => {Console.WriteLine(5);} 
    public MainWindowVM()
    {
        var abrirSalaDeChat = new Comando(sala => MostrarVista(_salaDeChat, sala));
        var abrirCrearSalaDeChat = new Comando(_ =>
        {
            MostrarVista(_crearSala);
        });
        var abrirRegistroDeUsuario = new Comando(_ => MostrarVista(_registroDeUsuario));
        var abrirListaDeSalas = new Comando(_ =>
        {
            MostrarVista(_listaDeSalas);
        });


        var registroDeUsuarioVM = new RegistroDeUsuarioVM(abrirListaDeSalas);
        _registroDeUsuario = new RegistroDeUsuario() { DataContext = registroDeUsuarioVM };
        _listaDeSalas = new ListaDeSalas() { DataContext = new ListaDeSalasVM(abrirCrearSalaDeChat, abrirSalaDeChat, abrirRegistroDeUsuario) { Usuario = registroDeUsuarioVM.Usuario } };
        _crearSala = new CrearSala() { DataContext = new CrearSalaVM(abrirSalaDeChat, abrirListaDeSalas) { Usuario = registroDeUsuarioVM.Usuario } };
        _salaDeChat = new SalaDeChat() { DataContext = new SalaDeChatVM(abrirCrearSalaDeChat, abrirListaDeSalas) { Usuario = registroDeUsuarioVM.Usuario } };

        //comandos
        _vistaActual = _registroDeUsuario;
        ConectarAlServidor();
    }
    private void RecibirSala(string nombre)
    {
        //lambdda//nombre => MessageBox.Show(nombre)
        //_listaDeSalas.AgregarSala(nombre);
       // _listaDeSalas.listaDeSalasVM.AgregarSala(nombre);
         if (_listaDeSalas.DataContext is ListaDeSalasVM vm){
                vm.AgregarSala(nombre);

         }
    }

    private void MostrarVista(UserControl vista, object? modelo = null)
    {
        VistaActual = vista;
        if (vista.DataContext is IVM vm)
        {
            vm.CargarModelo(modelo);
            //230
        }
    }

    private async void ConectarAlServidor()
    {
        _connection = new HubConnectionBuilder() // establece conexcion
                .WithUrl("http://localhost:5037/Chat")
                .Build();
        _connection.On<string>("RecibirSala", RecibirSala);
        try
        {
            await _connection.StartAsync(); // empieza conexion 
            //await _connection.InvokeAsync("?????", "x");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }


}