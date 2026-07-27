namespace Chat.Base.DTO;

public sealed class UsuarioDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public required string Nombre { get; set; }
    public string? Color { get; set; }
}