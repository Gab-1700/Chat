using Chat.Vista;

namespace Chat.Cliente.Comandos;

public class AbrirCrearSala : AbrirVista<CrearSala>
{
    public AbrirCrearSala(IServiceProvider proveedor) : base(proveedor) { }
}