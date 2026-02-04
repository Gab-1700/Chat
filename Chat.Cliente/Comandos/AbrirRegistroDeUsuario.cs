using Chat.Cliente.VistaModelo;
using Chat.Vista;

namespace Chat.Cliente.Comandos;

public class AbrirRegistroDeUsuario : AbrirVista<RegistroDeUsuario>
{
    public AbrirRegistroDeUsuario(IServiceProvider proveedor) : base(proveedor){}
}