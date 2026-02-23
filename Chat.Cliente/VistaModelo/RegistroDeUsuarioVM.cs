using System.Windows.Input;
using Chat.Cliente.Comandos;
using Chat.Base.Modelo;
using Chat.Services;
using Chat.Vista;
using System.Net.Http;
using System.Net.Http.Json;

namespace Chat.Cliente.VistaModelo;

public class RegistroDeUsuarioVM : BaseVM<object>
{
    public Usuario Usuario { get; }
    public ICommand AbrirListaDeSala { get; }

    public RegistroDeUsuarioVM(NavigationService navigationService, HttpClient httpClient)
    {
        Usuario = new() { Nombre = "", Color = "#000000" };
        AbrirListaDeSala = new Comando(
            async _ =>
            {
                try
                {
                    await httpClient.PostAsJsonAsync("api/usuarios", Usuario);
                    navigationService.NavigateTo<ListaDeSalas, Usuario>(Usuario);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error en la solicitud: {ex}");
                }
            },
            () => !string.IsNullOrEmpty(Usuario.Nombre)
        );

    }
}