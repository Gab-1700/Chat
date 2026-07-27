namespace Chat.Servidor.Entidades;

public sealed class Sala
{
    public Guid Id { get; set; } = Guid.Empty;
    public required string Nombre { get; set; }
    public int Capacidad { get; set; }
    public string? Contraseña { get; set; }
    public required Guid AnfitrionId { get; set; }
    public Usuario Anfitrion { get; set; } = null!;
    public List<Usuario> Usuarios { get; set; } = [];
    public List<Mensaje> Mensajes { get; set; } = [];
}
