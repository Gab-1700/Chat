using Chat.Cliente.VistaModelo;
using Chat.Vista;

namespace Chat.Cliente.Comandos;

public class AbrirSalaDeChat : AbrirVista<SalaDeChat>
{
    public AbrirSalaDeChat(IServiceProvider proveedor) : base(proveedor) { }
}