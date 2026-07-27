using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using Chat.Base.DTO;
using Chat.Cliente.Comandos;
using Chat.Services;
using Chat.Vista;
using Microsoft.Extensions.Logging;

namespace Chat.Cliente.VistaModelo;

public class CrearSalaVM : BaseVM<UsuarioDto>
{
    public UsuarioDto? Usuario { get; private set; }
    public SalaDto Sala { get; }
    public ICommand AbrirSalaDeChat { get; }
    public ICommand AbrirListaDeSalas { get; }

    public CrearSalaVM(NavigationService navigationService, HttpClient httpClient,ILogger<CrearSalaVM> logger)
    {
        Sala = new() { Nombre = "", AnfitrionId = Guid.Empty };
        AbrirListaDeSalas = new Comando(_ => navigationService.NavigateTo<ListaDeSalas>());
        AbrirSalaDeChat = new Comando(
            async _ =>
            {
                try
                {
                    if (Guid.Empty == Sala.Id)
                    {
                        var response = await httpClient.PostAsJsonAsync("api/salas", Sala);
                        if (response.IsSuccessStatusCode)
                        {
                            var nuevaSala = await response.Content.ReadFromJsonAsync<SalaDto>();
                            Sala.Id = nuevaSala!.Id;
                        }
                    }
                    else
                    {
                        await httpClient.PutAsJsonAsync($"api/salas/{Sala.Id}", Sala);
                    }
                    navigationService.NavigateTo<SalaDeChat, (UsuarioDto, SalaDto)>((Usuario, Sala));
                }
                catch (Exception ex)
                {
                    logger.LogError("Error en la solicitud: {ex}", ex.Message);
                }
            },
            () => !string.IsNullOrEmpty(Sala.Nombre)
        );
    }

    public override Task LoadAsync(UsuarioDto? usuario)
    {
        Usuario = usuario;
        Sala.AnfitrionId = usuario!.Id;
        return Task.CompletedTask;
    }
}