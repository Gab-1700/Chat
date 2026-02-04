namespace Chat.Base.Modelo;

public sealed class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Nombre { get; set; }
    public string? Imagen { get; set; }
    public string? Color { get; set; }
    public Guid? SalaId { get; set; }
    public Sala? Sala { get; set; }
    public List<Mensaje> Mensajes { get; set; } = [];
}