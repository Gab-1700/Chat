using System.Windows.Input;
using System.Windows.Media;
using Chat.Modelo;

namespace Chat.VistaModelo;

public class RegistroDeUsuarioVM : BaseVM
{
    public Usuario Usuario { get; set; }
    public ICommand AbrirListaDeSala { get; set; }

    public RegistroDeUsuarioVM(ICommand abrirListaDeSala)
    {
       // AbrirListaDeSala = abrirListaDeSala;
        AbrirListaDeSala = new Comando( abrirListaDeSala.Execute,PuedesNavegar);
        Usuario = new Usuario(){Color=Brushes.Black};
        //Usuario.Color = Brushes.Black;

    }
    public bool PuedesNavegar()
    {
        return !(Usuario.Nombre == null || Usuario.Nombre == "");
        //return !string.IsNullOrEmpty(Usuario.Nombre);
        // return Usuario.Nombre != "" && Usuario.Nombre != null ;

    }

    public void CanExecute(object? objeto){
        PuedesNavegar();
    }
}