namespace Chat.Servidor.Entidades;

public sealed class Mensaje
{
    public Guid Id { get; set; } = Guid.Empty;
    public required string Texto { get; set; }
    public DateTimeOffset Fecha { get; set; } = DateTimeOffset.UtcNow;
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public Guid SalaId { get; set; }
    public Sala? Sala { get; set; }
}
