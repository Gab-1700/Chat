using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;

using Chat.Modelo;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Microsoft.VisualBasic;
namespace Chat.VistaModelo;

public class CrearSalaVM : BaseVM
{
    public Sala Sala { get; set; }

    public Usuario Usuario { get; set; }

    public ICommand AbrirSalaDeChat { get; set; }
    public ICommand AbrirListaDeSalas { get; set; }


    public CrearSalaVM(ICommand abriSalaDeChat, ICommand abrirListaDeSalas)
    {
        // navegar = new Comando(_ => abriSalaDeChat.Execute(_),PuedesNavegar);
        AbrirListaDeSalas = abrirListaDeSalas;
        AbrirSalaDeChat = new Comando(modelo =>
        {
            // serviro creee sala con ese nombre 
            
            abriSalaDeChat.Execute(modelo);
        }, PuedesNavegar);
         //_conection.InvokeAsync("EnviarSala",Sala.Nombre);
        Sala = new Sala();

    }
    public bool PuedesNavegar()
    {
        return !(Sala.Nombre == null || Sala.Nombre == "");
    }

}