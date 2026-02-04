using Chat.Base.Modelo;
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
            await service.JoinSala(salaId, usuarioId);
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Sala:{salaId}");
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
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Sala:{salaId}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new HubException(ex.Message);
        }
    }
}