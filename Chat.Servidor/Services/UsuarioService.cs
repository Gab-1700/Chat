using Chat.Base.Modelo;
using Chat.Servidor.Data;
using Microsoft.EntityFrameworkCore;

namespace Chat.Servidor.Services;

public sealed class UsuarioService(ChatDbContext dbContext, ILogger<SalaService> logger)
{
    public async Task<Usuario> CreateUsuario(Usuario usuario)
    {
        dbContext.Usuarios.Add(usuario);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Usuario: '{UsuarioId}' creado correctamente", usuario.Id);

        return usuario;
    }

    public async Task<Usuario?> UpdateUsuario(Usuario usuario)
    {
        var usuarioExistente = await dbContext.Usuarios
            .FirstOrDefaultAsync(u => u.Id == usuario.Id);
        if (usuarioExistente is null)
            return null;

        dbContext.Entry(usuarioExistente)
            .CurrentValues
            .SetValues(usuario);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Usuario: '{UsuarioId}' actualizado correctamente", usuario.Id);

        return usuarioExistente;
    }
}
