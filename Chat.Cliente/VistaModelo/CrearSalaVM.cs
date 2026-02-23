using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using Chat.Base.Modelo;
using Chat.Cliente.Comandos;
using Chat.Services;
using Chat.Vista;

namespace Chat.Cliente.VistaModelo;

public class CrearSalaVM : BaseVM<Usuario>
{
    public Usuario? Usuario { get; private set; }
    public Sala Sala { get; }
    public ICommand AbrirSalaDeChat { get; }
    public ICommand AbrirListaDeSalas { get; }

    public CrearSalaVM(NavigationService navigationService, HttpClient httpClient)
    {
        Sala = new() { Nombre = "", AnfitrionId = Guid.Empty };
        AbrirListaDeSalas = new Comando(_ => navigationService.NavigateTo<ListaDeSalas>());
        AbrirSalaDeChat = new Comando(
            async _ =>
            {
                await httpClient.PostAsJsonAsync("api/salas", Sala);
                navigationService.NavigateTo<SalaDeChat, (Usuario, Sala)>((Usuario, Sala));
            },
            () => !string.IsNullOrEmpty(Sala.Nombre)
        );
    }

    public override Task LoadAsync(Usuario? usuario)
    {
        Usuario = usuario;
        Sala.AnfitrionId = usuario.Id;
        return Task.CompletedTask;
    }
}