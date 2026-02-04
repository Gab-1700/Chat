using Chat.Base.Modelo;
using Chat.Servidor.Data;
using Chat.Servidor.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Servidor.Services;

public class MensajeService(ChatDbContext dbContext, IHubContext<ChatHub> hubContext, ILogger<SalaService> logger)
{
    public async Task<Mensaje> CrearMensaje(Guid salaId, Mensaje mensaje)
    {
        mensaje.SalaId = salaId;
        dbContext.Mensajes.Add(mensaje);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Mensaje: '{MensajeId}' creado correctamente en la sala '{salaId}'", mensaje.Id, salaId);

        await hubContext.Clients
            .Group($"Sala:{salaId}")
            .SendAsync("CrearMensaje", mensaje);
        logger.LogInformation("Grupo: 'Sala:{Id}' fue notificado correctamente", salaId);

        return mensaje;
    }

    public async Task<List<Mensaje>> GetMensajesPorSala(Guid salaId)
    {
        return await dbContext.Mensajes
            .AsNoTracking()
            .Where(m => m.SalaId == salaId)
            .OrderByDescending(m => m.Id)
            .Take(25)
            .ToListAsync();
    }
}
