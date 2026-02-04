using Chat.Base.Modelo;
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

            group.MapPost("/", CreateUsuario);
            group.MapPut("/{id}", UpdateUsuario);
            return group;
        }
    }

    private static async Task<IResult> CreateUsuario(Usuario usuario, UsuarioService service)
    {
        try
        {
            await service.CreateUsuario(usuario);
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> UpdateUsuario(Guid id, Usuario usuario, UsuarioService service)
    {
        try
        {
            usuario.Id = id;
            return Results.Ok(await service.UpdateUsuario(usuario));
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    }
}
