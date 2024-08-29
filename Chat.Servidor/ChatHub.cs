using Microsoft.AspNetCore.SignalR;

namespace Chat;


public class ChatHub : Hub
{   
    private readonly ILogger _logger;
    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public async Task EnviarSala(string nombre) // nos llega el nombre de la sala
                                        // para enviar al hub tiene q tener el metodo
    {
        _logger.LogInformation("se ha ingresado 'EnviarSala' con el nombre : " + nombre);
        await Clients.All.SendAsync("RecibirSala", "la sala :" + nombre + "llego correctamente");
    }
}