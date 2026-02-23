using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Chat.Cliente.Comandos;
using Chat.Base.Modelo;
using Microsoft.AspNetCore.SignalR.Client;
using Chat.Services;
using Chat.Vista;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace Chat.Cliente.VistaModelo;
// namespace Chat.Base.Modelo;
public class SalaDeChatVM : BaseVM<(Usuario, Sala)>
{
    private string _mensajeTxt;
    private HttpClient _httpClient;
    private HubConnection _connection;
    public ObservableCollection<Mensaje> Mensajes { get; }
    public ObservableCollection<Usuario> Usuarios { get; }
    public ICommand AbrirCrearSala { get; }
    public ICommand AbrirListaDeSalas { get; }
    public ICommand EnviarMensaje { get; }
    public Usuario Usuario { get; private set; }
    public Sala Sala { get; private set; }
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


    public SalaDeChatVM(HubConnection conexion, NavigationService navigationService, HttpClient httpClient,ILogger<SalaDeChatVM> logger)
    {
        Mensajes = [];
        Usuarios = [];
        _httpClient = httpClient;
        _connection = conexion;
        AbrirCrearSala = new Comando(_ => navigationService.NavigateTo<CrearSala, Usuario>(Usuario));
        AbrirListaDeSalas = new Comando(_ => navigationService.NavigateTo<ListaDeSalas, Usuario>(Usuario));
        EnviarMensaje = new Comando(async _ =>
        {
            try
            {
                var mensaje = new Mensaje { Texto = MensajeTxt, UsuarioId = Usuario.Id, Fecha = DateTime.Now };
                await httpClient.PostAsJsonAsync($"api/salas/{Sala.Id}/mensajes", mensaje);

                MensajeTxt = string.Empty;

            }
            catch (Exception ex)
            {
                logger.LogError($"Error en la solicitud: {ex}");
            }

        });

        conexion.On<Mensaje>("CrearMensaje", (mensaje) =>           //recibo del servidor 
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Mensajes.Add(mensaje);
            });
        });
    }

    public override async Task LoadAsync((Usuario, Sala) args)
    {
        var (usuario, sala) = args;
        Usuario = usuario;
        Sala = sala;



        Mensajes.Clear();
        var mensajes = await _httpClient.GetFromJsonAsync<List<Mensaje>>($"api/salas/{Sala.Id}/mensajes");
        foreach (var mensaje in mensajes!)
        {
            Mensajes.Add(mensaje);
        }
        await _connection.InvokeAsync("JoinSala", sala.Id, usuario.Id);
    }





}