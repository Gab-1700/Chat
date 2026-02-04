using System.Windows.Input;
using System.Windows.Media;
using Chat.Cliente.Comandos;
using Chat.Base.Modelo;

namespace Chat.Cliente.VistaModelo;

public class RegistroDeUsuarioVM : BaseVM
{
    public Usuario Usuario { get; set; }
    public ICommand AbrirListaDeSala { get; private set; }

    public RegistroDeUsuarioVM(AbrirListaDeSalas abrirListaDeSalas)
    {
        AbrirListaDeSala = new Comando(abrirListaDeSalas.Execute, PuedesNavegar);
        Usuario = new Usuario() { Color = "#000000" };
    }

    public bool PuedesNavegar()
    {
        return !(Usuario.Nombre == null || Usuario.Nombre == "");
        //return !string.IsNullOrEmpty(Usuario.Nombre);
        // return Usuario.Nombre != "" && Usuario.Nombre != null ;

    }

    public void CanExecute(object? objeto)
    {
        PuedesNavegar();
    }
}