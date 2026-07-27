using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Chat.Cliente.Comandos;
using Microsoft.AspNetCore.SignalR.Client;
using Chat.Services;
using Chat.Vista;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Chat.Base.DTO;

namespace Chat.Cliente.VistaModelo;
// namespace Chat.Base.Modelo;
public class SalaDeChatVM : BaseVM<(UsuarioDto, SalaDto)>
{
    private string _mensajeTxt;
    private HttpClient _httpClient;
    private HubConnection _connection;
    public ObservableCollection<MensajeDto> Mensajes { get; } = [];
    public ObservableCollection<UsuarioDto> Usuarios { get; } = [];
    public ICommand AbrirCrearSala { get; }
    public ICommand AbrirListaDeSalas { get; }
    public ICommand EnviarMensaje { get; }
    public Visibility Anfitrion { get; private set; }
    public Visibility Visitante { get; private set; }
    public UsuarioDto Usuario { get; private set; }
    public SalaDto Sala { get; private set; }
    public string MensajeTxt
    {
        get => _mensajeTxt;
        set
        {
            if (_mensajeTxt != value)
            {
                _mensajeTxt = value;
                OnPropertyChanged(nameof(MensajeTxt));
            }
        }
    }


    public SalaDeChatVM(HubConnection conexion, NavigationService navigationService, HttpClient httpClient, ILogger<SalaDeChatVM> logger)
    {
        _httpClient = httpClient;
        _connection = conexion;
        AbrirCrearSala = new Comando(async _ =>
        {
            await LeaveSala();
            navigationService.NavigateTo<CrearSala, UsuarioDto>(Usuario);
        });
        AbrirListaDeSalas = new Comando(async _ =>
        {
            await LeaveSala();
            navigationService.NavigateTo<ListaDeSalas, UsuarioDto>(Usuario);
        });
        EnviarMensaje = new Comando(async _ =>
        {
            try
            {
                var mensaje = new MensajeDto { Texto = MensajeTxt, UsuarioId = Usuario.Id, Fecha = DateTime.Now };
                await httpClient.PostAsJsonAsync($"api/salas/{Sala.Id}/mensajes", mensaje);

                MensajeTxt = string.Empty;
            }
            catch (Exception ex)
            {
                logger.LogError("Error en la solicitud: {ex}", ex.Message);
            }

        });

        conexion.On<MensajeDto>("CrearMensaje", CrearMensaje);
        conexion.On<UsuarioDto>("AgregarUsuario", AgregarUsuario);
        conexion.On<Guid>("EliminarUsuario", EliminarUsuario);
    }

    public override async Task LoadAsync((UsuarioDto, SalaDto) args)
    {
        Usuario = args.Item1;
        Sala = args.Item2;

        if (Sala.AnfitrionId == Usuario.Id)
        {
            Anfitrion = Visibility.Visible;
            Visitante = Visibility.Collapsed;
        }
        else
        {
            Anfitrion = Visibility.Collapsed;
            Visitante = Visibility.Visible;
        }

        Mensajes.Clear();
        var mensajes = await _httpClient.GetFromJsonAsync<List<MensajeDto>>($"api/salas/{Sala.Id}/mensajes");
        foreach (var mensaje in mensajes!)
        {
            Mensajes.Add(mensaje);
        }

        Usuarios.Clear();
        var usuarios = await _httpClient.GetFromJsonAsync<List<UsuarioDto>>($"api/salas/{Sala.Id}/usuarios");
        foreach (var usuario in usuarios!)
        {
            Usuarios.Add(usuario);
        }

        await JoinSala();
    }

    private async Task JoinSala()
    {
        await _connection.InvokeAsync("JoinSala", Sala.Id, Usuario.Id);
    }

    private async Task LeaveSala()
    {
        await _connection.InvokeAsync("LeaveSala", Sala.Id, Usuario.Id);
    }

    private void CrearMensaje(MensajeDto mensaje)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Mensajes.Add(mensaje);
        });
    }

    private void AgregarUsuario(UsuarioDto usuario)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Usuarios.Add(usuario);
        });
    }

    private void EliminarUsuario(Guid usuarioId)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario != null)
            {
                Usuarios.Remove(usuario);
            }
        });
    }

}