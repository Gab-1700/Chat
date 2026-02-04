using Chat.Base.Modelo;
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

            group.MapGet("/{id}/mensajes", GetMensajes);
            group.MapPost("/{id}/mensajes", CreateMensaje);

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

    private static async Task<IResult> CreateSala(Sala sala, SalaService service)
    {
        try
        {
            await service.CreateSala(sala);
            return Results.Created($"/api/salas/{sala.Id}", sala);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> UpdateSala(Guid id, Sala sala, SalaService service)
    {
        try
        {
            sala.Id=id;
            return Results.Ok(await service.UpdateSala(sala));
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


    private static async Task<IResult> GetMensajes(Guid id, MensajeService service)
    {
        return Results.Ok(await service.GetMensajesPorSala(id));
    }

    private static async Task<IResult> CreateMensaje(Guid id, Mensaje mensaje, MensajeService service)
    {
        try
        {
            await service.CrearMensaje(id, mensaje);
            return Results.Created($"/api/salas/{id}/mensajes/{mensaje.Id}", mensaje);
        }
        catch (Exception ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }
}