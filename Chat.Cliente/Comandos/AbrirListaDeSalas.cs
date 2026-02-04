using Chat.Cliente.VistaModelo;
using Chat.Vista;

namespace Chat.Cliente.Comandos;

public class AbrirListaDeSalas : AbrirVista<ListaDeSalas>
{
    public AbrirListaDeSalas (IServiceProvider proveedor) : base(proveedor) { }
}