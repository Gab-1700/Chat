using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using Chat.Cliente.Comandos;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
using System.Net.Http.Json;
using Chat.Services;
using Chat.Vista;
using Chat.Base.DTO;

namespace Chat.Cliente.VistaModelo;

public class ListaDeSalasVM : BaseVM<UsuarioDto>
{
    private HttpClient _httpClient;
    private HubConnection _connection;
    public ObservableCollection<SalaDto> Salas { get; } = [];
    public UsuarioDto? Usuario { get; private set; }
    public ICommand AbrirSalaDeChat { get; }
    public ICommand AbrirCrearSalaDeChat { get; }
    public ICommand AbrirRegistroDeUsuario { get; }

    public ListaDeSalasVM(HubConnection conexion, NavigationService navigationService, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _connection = conexion;
        AbrirRegistroDeUsuario = new Comando(async _ =>
        {
            await LeaveListaSalas();
            navigationService.NavigateTo<RegistroDeUsuario>();
        });
        AbrirCrearSalaDeChat = new Comando(async _ =>
        {
            await LeaveListaSalas();
            navigationService.NavigateTo<CrearSala, UsuarioDto>(Usuario);
        });
        AbrirSalaDeChat = new Comando(async sala =>
        {
            await LeaveListaSalas();
            navigationService.NavigateTo<SalaDeChat, (UsuarioDto, SalaDto)>((Usuario, sala as SalaDto));
        });
        _connection.On<SalaDto>("CrearSala", ActualizarSalas);
    }

    public override async Task LoadAsync(UsuarioDto? usuario)
    {
        Usuario = usuario;
        var salas = await _httpClient.GetFromJsonAsync<List<SalaDto>>("api/salas");
        Salas.Clear();
        foreach (var sala in salas!)
        {
            Salas.Add(sala);
        }
        await JoinListaSalas();
    }

    private async Task JoinListaSalas()
    {
        await _connection.InvokeAsync("JoinListaSalas");  //suscribirse
    }
    private async Task LeaveListaSalas()
    {
        await _connection.InvokeAsync("LeaveListaSalas");
    }

    private void ActualizarSalas(SalaDto sala)
    {
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Salas.Add(sala);
        });
    }


}