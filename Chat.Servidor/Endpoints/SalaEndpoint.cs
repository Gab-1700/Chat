using Chat.Base.DTO;
using Chat.Servidor.Services;

namespace Chat.Servidor.Endpoints;

public static class SalaEndpoint
{
    extension(IEndpointRouteBuilder routes)
    {

        public RouteGroupBuilder MapSalaEndpoint()
        {
            var group = routes.MapGroup("/api/salas")
                .WithTags("Salas");

            group.MapGet("/", GetSalas);
            group.MapGet("/{id}", GetSalaPorId);
            group.MapPost("/", CreateSala);
            group.MapPut("/{id}", UpdateSala);
            group.MapDelete("/{id}", DeleteSala);

            group.MapGet("/{salaId}/mensajes", GetMensajesPorSala);
            group.MapPost("/{salaId}/mensajes", CreateMensajePorSala);

            group.MapGet("/{salaId}/usuarios", GetUsuariosPorSala);

            return group;
        }
    }

    private static async Task<IResult> GetSalas(SalaService service)
    {
        return Results.Ok(await service.GetSalas());
    }

    private static async Task<IResult> GetSalaPorId(Guid id, SalaService service)
    {
        try
        {
            return Results.Ok(await service.GetSalaPorId(id));
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }

    private static async Task<IResult> CreateSala(SalaDto salaDto, SalaService service)
    {
        try
        {
            await service.CreateSala(salaDto);
            return Results.Created($"/api/salas/{salaDto.Id}", salaDto);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> UpdateSala(Guid id, SalaDto salaDto, SalaService service)
    {
        try
        {
            return Results.Ok(await service.UpdateSala(id,salaDto));
        }
        catch (Exception ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }

    private static async Task<IResult> DeleteSala(
        Guid id, SalaService service)
    {
        return await service.DeleteSala(id)
            ? Results.NoContent()
            : Results.NotFound();
    }


    private static async Task<IResult> GetMensajesPorSala(Guid salaId, MensajeService service)
    {
        return Results.Ok(await service.GetMensajesPorSala(salaId));
    }


    private static async Task<IResult> CreateMensajePorSala(Guid salaId, MensajeDto mensajeDto, MensajeService service)
    {
        try
        {
            await service.CrearMensajePorSala(salaId, mensajeDto);
            return Results.Created($"/api/salas/{salaId}/mensajes/{mensajeDto.Id}", mensajeDto);
        }
        catch (Exception ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }

    private static async Task<IResult> GetUsuariosPorSala(Guid salaId, UsuarioService service)
    {
        return Results.Ok(await service.GetUsuariosPorSala(salaId));
    }
}