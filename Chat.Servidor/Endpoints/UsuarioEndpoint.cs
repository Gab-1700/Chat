using Chat.Base.DTO;
using Chat.Servidor.Services;

namespace Chat.Servidor.Endpoints;

public static class UsuarioEndpoint
{
    extension(IEndpointRouteBuilder routes)
    {
        public RouteGroupBuilder MapUsuarioEndpoint()
        {
            var group = routes.MapGroup("/api/usuarios")
                .WithTags("Usuarios");

            group.MapGet("/", GetUsuario);
            group.MapPost("/", CreateUsuario);
            group.MapPut("/{id}", UpdateUsuario);
            return group;
        }
    }

    private static async Task<IResult> GetUsuario(UsuarioService service)
    {
        return Results.Ok(await service.GetUsuarios());
    }

    private static async Task<IResult> CreateUsuario(UsuarioDto usuarioDto, UsuarioService service)
    {
        try
        {
            await service.CreateUsuario(usuarioDto);
            return Results.Created($"/api/usuario/{usuarioDto.Id}", usuarioDto);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> UpdateUsuario(Guid id, UsuarioDto usuarioDto, UsuarioService service)
    {
        try
        {
            return Results.Ok(await service.UpdateUsuario(id,usuarioDto));
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }
}
