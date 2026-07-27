using Chat.Base.DTO;
using Chat.Servidor.Data;
using Chat.Servidor.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Chat.Servidor.Services;

public sealed class UsuarioService(ChatDbContext dbContext, ILogger<SalaService> logger)
{
    public async Task<List<UsuarioDto>> GetUsuarios()
    {
        return await dbContext.Usuarios
            .AsNoTracking()
            .Select(u => ToDto(u))
            .ToListAsync();
    }
    public async Task<UsuarioDto> GetUsuarioPorId(Guid usuarioId)
    {
        return await dbContext.Usuarios
           .AsNoTracking()
           .Where(u => u.Id == usuarioId)
           .Select(u => ToDto(u))
           .FirstOrDefaultAsync()
           ?? throw new KeyNotFoundException($"Usuario: '{usuarioId}' no fue encontrado.");
    }
    public async Task<List<UsuarioDto>> GetUsuariosPorSala(Guid salaId)
    {
        return await dbContext.Usuarios
            .AsNoTracking()
            .Where(u => u.SalaId == salaId)
            .Select(u => ToDto(u))
            .ToListAsync();
    }

    public async Task<UsuarioDto> CreateUsuario(UsuarioDto usuarioDto)
    {
        var usuario = new Usuario() { Nombre = usuarioDto.Nombre, Color = usuarioDto.Color };
        dbContext.Usuarios.Add(usuario);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Usuario: '{UsuarioId}' creado correctamente", usuario.Id);

        usuarioDto.Id = usuario.Id;
        return usuarioDto;
    }

    public async Task<UsuarioDto> UpdateUsuario(Guid usuarioId, UsuarioDto usuarioDto)
    {
        var usuario = await dbContext.Usuarios
            .FirstOrDefaultAsync(u => u.Id == usuarioId)
            ?? throw new KeyNotFoundException($"Usuario: '{usuarioId}' no fue encontrado.");

        usuario.Nombre = usuarioDto.Nombre;
        usuario.Color = usuarioDto.Color;
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Usuario: '{UsuarioId}' actualizado correctamente", usuarioId);

        usuarioDto.Id = usuarioId;
        return usuarioDto;
    }

    private static UsuarioDto ToDto(Usuario usuario)
    {
        return new() { Id = usuario.Id, Nombre = usuario.Nombre, Color = usuario.Color };
    }
}
