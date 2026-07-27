using System.Windows.Input;
using Chat.Cliente.Comandos;
using Chat.Services;
using Chat.Vista;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Chat.Base.DTO;

namespace Chat.Cliente.VistaModelo;

public class RegistroDeUsuarioVM : BaseVM<object>
{
    public UsuarioDto Usuario { get; }
    public ICommand AbrirListaDeSala { get; }

    public RegistroDeUsuarioVM(NavigationService navigationService, HttpClient httpClient, ILogger<RegistroDeUsuarioVM> logger)
    {
        Usuario = new() { Nombre = "", Color = "#000000" };
        AbrirListaDeSala = new Comando(
            async _ =>
            {
                try
                {
                    if (Guid.Empty == Usuario.Id)
                    {
                        var response = await httpClient.PostAsJsonAsync("api/usuarios", Usuario);
                        if (response.IsSuccessStatusCode)
                        {
                            var nuevoUsuario = await response.Content.ReadFromJsonAsync<UsuarioDto>();
                            Usuario.Id = nuevoUsuario!.Id;
                        }
                    }
                    else
                    {
                        await httpClient.PutAsJsonAsync($"api/usuarios/{Usuario.Id}", Usuario);
                    }

                    navigationService.NavigateTo<ListaDeSalas, UsuarioDto>(Usuario);
                }
                catch (Exception ex)
                {
                    logger.LogError("Error en la solicitud: {ex}", ex.Message);
                }
            },
            () => !string.IsNullOrEmpty(Usuario.Nombre)
        );

    }
}