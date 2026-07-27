namespace Chat.Base.DTO;

public sealed class MensajeDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public required string Texto { get; set; }
    public DateTimeOffset Fecha { get; set; } = DateTimeOffset.UtcNow;
    public Guid UsuarioId { get; set; }
    public UsuarioDto Usuario { get; set; } = null!;
    public Guid SalaId { get; set; }
}