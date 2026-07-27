using Chat.Base.DTO;
using Chat.Servidor.Data;
using Chat.Servidor.Entidades;
using Chat.Servidor.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace Chat.Servidor.Services;

public sealed class SalaService(ChatDbContext dbContext, IHubContext<ChatHub> hubContext, ILogger<SalaService> logger)
{
    public async Task<SalaDto> CreateSala(SalaDto salaDto)
    {
        var sala = ToEntity(salaDto);
        dbContext.Salas.Add(sala);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Sala: '{SalaId}' creada correctamente", sala.Id);

        salaDto.Id = sala.Id;
        await hubContext.Clients
            .Group("ListaSalas")
            .SendAsync("CrearSala", salaDto);
        logger.LogInformation("Grupo: 'ListaSalas' fue notificado correctamente");

        return salaDto;
    }
    public async Task<List<SalaDto>> GetSalas()
    {
        return await dbContext.Salas
            .AsNoTracking()
            .Select(s => ToDto(s))
            .ToListAsync();
    }

    public async Task<SalaDto> GetSalaPorId(Guid id)
    {
        return await dbContext.Salas
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => ToDto(s))
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"Sala: '{id}' no fue encontrada.");
    }

    public async Task<SalaDto> UpdateSala(Guid salaId, SalaDto salaDto)
    {
        var sala = await dbContext.Salas
            .FirstOrDefaultAsync(s => s.Id == salaId)
            ?? throw new KeyNotFoundException($"Sala: '{salaId}' no fue encontrada."); ;

        sala.Nombre = salaDto.Nombre;
        sala.Capacidad = salaDto.Capacidad;
        sala.Contraseña = salaDto.Contraseña;
        sala.AnfitrionId = salaDto.AnfitrionId;
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Sala: '{SalaId}' actualizada correctamente", salaId);

        salaDto.Id=salaId;
        await hubContext.Clients
            .Group("ListaSalas")
            .SendAsync("ActualizarSala", salaDto);
        logger.LogInformation("Grupo: 'ListaSalas' fue notificado correctamente");

        return salaDto;
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

    public async Task<UsuarioDto> JoinSala(Guid salaId, Guid usuarioId)
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
        return new() {Id=usuario.Id,Nombre=usuario.Nombre,Color=usuario.Color};
    }

    public async Task LeaveSala(Guid salaId, Guid usuarioId)
    {
        var usuario = await dbContext.Usuarios
            .FirstOrDefaultAsync(u => u.Id == usuarioId)
            ?? throw new KeyNotFoundException($"No existe el usuario con ID {usuarioId}.");

        var sala = await dbContext.Salas
            .FirstOrDefaultAsync(s => s.Id == salaId)
            ?? throw new KeyNotFoundException($"No existe la sala con ID {salaId}.");

        usuario.SalaId = null;
        await dbContext.SaveChangesAsync();
    }

    private Sala ToEntity(SalaDto salaDto)
    {
        return new() { Nombre = salaDto.Nombre, Capacidad = salaDto.Capacidad, Contraseña = salaDto.Contraseña, AnfitrionId = salaDto.AnfitrionId };
    }

    private static SalaDto ToDto(Sala sala)
    {
        return new() { Id = sala.Id, Nombre = sala.Nombre, Capacidad = sala.Capacidad, Contraseña = sala.Contraseña, AnfitrionId = sala.AnfitrionId };
    }
}


