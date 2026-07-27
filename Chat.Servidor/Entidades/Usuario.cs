namespace Chat.Servidor.Entidades;

public sealed class Usuario
{
    public Guid Id { get; set; } = Guid.Empty;
    public required string Nombre { get; set; }
    public string? Color { get; set; }
    public Guid? SalaId { get; set; }
    public Sala? Sala { get; set; }
    public List<Mensaje> Mensajes { get; set; } = [];
}
