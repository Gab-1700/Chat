using Chat.Base.Modelo;
using Chat.Servidor.Data;
using Chat.Servidor.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace Chat.Servidor.Services;

public sealed class SalaService(ChatDbContext dbContext, IHubContext<ChatHub> hubContext, ILogger<SalaService> logger)
{
    public async Task<Sala> CreateSala(Sala sala)
    {
        dbContext.Salas.Add(sala);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Sala: '{SalaId}' creada correctamente", sala.Id);

        await hubContext.Clients
            .Group("ListaSalas")
            .SendAsync("CrearSala", sala);
        logger.LogInformation("Grupo: 'ListaSalas' fue notificado correctamente");

        return sala;
    }
    public async Task<List<Sala>> GetSalas()
    {
        return await dbContext.Salas
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Sala?> GetSalaPorId(Guid id)
    {
        return await dbContext.Salas
            .AsNoTracking()
            .Include(s => s.Anfitrion)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sala?> UpdateSala(Sala sala)
    {
        var salaExistente = await dbContext.Salas
            .FirstOrDefaultAsync(s => s.Id == sala.Id);
        if (salaExistente is null)
            return null;

        dbContext.Entry(salaExistente)
            .CurrentValues
            .SetValues(sala);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Sala: '{SalaId}' actualizada correctamente", sala.Id);

        await hubContext.Clients
            .Group("ListaSalas")
            .SendAsync("ActualizarSala", salaExistente);
        logger.LogInformation("Grupo: 'ListaSalas' fue notificado correctamente");

        return salaExistente;
    }

    public async Task<bool> DeleteSala(Guid id)
    {
        var sala = await dbContext.Salas
            .FirstOrDefaultAsync(s => s.Id == id);
        if (sala is null)
            return false;

        dbContext.Salas.Remove(sala);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Sala: '{SalaId}' eliminada correctamente", sala.Id);

        await hubContext.Clients
            .Group("ListaSalas")
            .SendAsync("EliminarSala", id);
        logger.LogInformation("Grupo: 'ListaSalas' fue notificado correctamente");
        return true;
    }

    public async Task JoinSala(Guid salaId, Guid usuarioId)
    {
        var usuario = await dbContext.Usuarios
            .FirstOrDefaultAsync(u => u.Id == usuarioId)
            ?? throw new KeyNotFoundException($"No existe el usuario con ID {usuarioId}.");

        var sala = await dbContext.Salas
            .Include(s => s.Usuarios)
            .FirstOrDefaultAsync(s => s.Id == salaId)
            ?? throw new KeyNotFoundException($"No existe la sala con ID {salaId}.");

        if (sala.Usuarios.Count >= sala.Capacidad)
            throw new InvalidOperationException("La sala ha alcanzado su capacidad máxima.");

        usuario.SalaId = sala.Id;
        await dbContext.SaveChangesAsync();
    }

    public async Task LeaveSala(Guid salaId, Guid usuarioId)
    {
        var usuario = await dbContext.Usuarios
            .FirstOrDefaultAsync(u => u.Id == usuarioId)
            ?? throw new KeyNotFoundException($"No existe el usuario con ID {usuarioId}.");

        var sala = await dbContext.Salas
            .FirstOrDefaultAsync(s => s.Id == salaId)
            ?? throw new KeyNotFoundException($"No existe la sala con ID {salaId}.");

        usuario.SalaId = Guid.Empty;
        await dbContext.SaveChangesAsync();
    }
}


