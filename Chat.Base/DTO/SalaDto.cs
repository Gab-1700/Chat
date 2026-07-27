namespace Chat.Base.DTO;

public sealed class SalaDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public required string Nombre { get; set; }
    public int Capacidad { get; set; }
    public string? Contraseña { get; set; }
    public required Guid AnfitrionId { get; set; }
    public UsuarioDto Anfitrion { get; set; } = null!;
}

