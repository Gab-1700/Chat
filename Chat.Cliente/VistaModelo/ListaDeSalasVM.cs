using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Chat.Base.Modelo;
using System.Collections.ObjectModel;
using Chat.Cliente.Comandos;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
using System.Net.Http.Json;
using Chat.Services;
using Chat.Vista;

namespace Chat.Cliente.VistaModelo;

public class ListaDeSalasVM : BaseVM<Usuario>
{
    private HttpClient _httpClient;
    private HubConnection _connection;
    public ObservableCollection<Sala> Salas { get; }
    public Usuario? Usuario { get; private set; }
    public ICommand AbrirSalaDeChat { get; }
    public ICommand AbrirCrearSalaDeChat { get; }
    public ICommand AbrirRegistroDeUsuario { get; }
    


    // algo por revisar
    public ListaDeSalasVM(HubConnection conexion, NavigationService navigationService, HttpClient httpClient)
    {
        Salas = [];
        _httpClient = httpClient;
        _connection = conexion;
        AbrirRegistroDeUsuario = new Comando(_ => navigationService.NavigateTo<RegistroDeUsuario>());
        AbrirCrearSalaDeChat = new Comando(_ => navigationService.NavigateTo<CrearSala, Usuario>(Usuario));
        AbrirSalaDeChat = new Comando(sala => navigationService.NavigateTo<SalaDeChat, (Usuario, Sala)>((Usuario, sala as Sala)));
        _connection.On<Sala>("CrearSala", ActualizarSalas);
    }

    public override async Task LoadAsync(Usuario? usuario)
    {
        Usuario = usuario;
        var salas = await _httpClient.GetFromJsonAsync<List<Sala>>("api/salas");
        Salas.Clear();
        foreach (var sala in salas!)
        {
            Salas.Add(sala);
        }
        await _connection.InvokeAsync("JoinListaSalas");
         
    }
    private void ActualizarSalas(Sala sala)
    {
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Salas.Add(sala);
        });
    }


}