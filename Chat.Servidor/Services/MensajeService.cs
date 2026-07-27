using Chat.Base.DTO;
using Chat.Servidor.Data;
using Chat.Servidor.Entidades;
using Chat.Servidor.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Servidor.Services;

public class MensajeService(ChatDbContext dbContext, IHubContext<ChatHub> hubContext, ILogger<SalaService> logger, UsuarioService usuarioService)
{
    public async Task<MensajeDto> CrearMensajePorSala(Guid salaId, MensajeDto mensajeDto)
    {
        mensajeDto.SalaId = salaId;
        dbContext.Mensajes.Add(ToEntity(mensajeDto));
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Mensaje: '{MensajeId}' creado correctamente en la sala '{salaId}'", mensajeDto.Id, salaId);

        mensajeDto.Usuario = await usuarioService.GetUsuarioPorId(mensajeDto.UsuarioId);
        await hubContext.Clients
            .Group($"Sala:{salaId}")
            .SendAsync("CrearMensaje", mensajeDto);
        logger.LogInformation("Grupo: 'Sala:{Id}' fue notificado correctamente", salaId);

        return mensajeDto;
    }

    public async Task<List<MensajeDto>> GetMensajesPorSala(Guid salaId)
    {
        return await dbContext.Mensajes
            .AsNoTracking()
            .Where(m => m.SalaId == salaId)
            .OrderByDescending(m => m.Id)
            .Take(25)
            .Select(m => new MensajeDto
            {
                Id = m.Id,
                Texto = m.Texto,
                Fecha = m.Fecha,
                UsuarioId = m.UsuarioId,
                SalaId = m.SalaId,
                Usuario = new UsuarioDto
                {
                    Id = m.Usuario.Id,
                    Nombre = m.Usuario.Nombre,
                    Color = m.Usuario.Color
                }
            })
            .ToListAsync();
    }

    private Mensaje ToEntity(MensajeDto mensajeDto)
    {
        return new() { Texto = mensajeDto.Texto, Fecha = mensajeDto.Fecha, UsuarioId = mensajeDto.UsuarioId, SalaId = mensajeDto.SalaId };
    }

}
