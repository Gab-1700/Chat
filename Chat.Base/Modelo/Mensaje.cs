namespace Chat.Base.Modelo;

public sealed class Mensaje
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Texto { get; set; }
    public DateTimeOffset Fecha { get; set; } = DateTimeOffset.UtcNow;
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public Guid SalaId { get; set; }
    public Sala? Sala { get; set; }
}