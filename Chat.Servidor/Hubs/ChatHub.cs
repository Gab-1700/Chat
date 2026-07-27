using Chat.Servidor.Services;
using Microsoft.AspNetCore.SignalR;
namespace Chat.Servidor.Hubs;

public class ChatHub(SalaService service, ILogger<ChatHub> logger) : Hub
{
    public async Task JoinListaSalas()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "ListaSalas");
    }

    public async Task LeaveListaSalas()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "ListaSalas");
    }

    public async Task JoinSala(Guid salaId, Guid usuarioId)
    {
        try
        {
            var usuarioDto = await service.JoinSala(salaId, usuarioId);
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Sala:{salaId}");
            await Clients.Group($"Sala:{salaId}")
                .SendAsync("AgregarUsuario", usuarioDto);
            logger.LogInformation("Grupo: 'Sala:{Id}' agrego al usuario: '{UsuarioId}' correctamente", salaId, usuarioId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new HubException(ex.Message);
        }
    }

    public async Task LeaveSala(Guid salaId, Guid usuarioId)
    {
        try
        {
            await service.LeaveSala(salaId, usuarioId);
            await Clients.Group($"Sala:{salaId}")
                .SendAsync("EliminarUsuario", usuarioId);
            logger.LogInformation("Grupo: 'Sala:{Id}' elimino al usuario: '{UsuarioId}' correctamente", salaId, usuarioId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Sala:{salaId}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new HubException(ex.Message);
        }
    }
}