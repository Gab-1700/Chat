using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Chat.Modelo;
using System.Collections.ObjectModel;

namespace Chat.VistaModelo;

public class ListaDeSalasVM :BaseVM{
     public ObservableCollection<Sala> Salas { get; set; }
    public Usuario Usuario { get; set; }
    public ICommand NavegacionACrearSala { get; set; }

    public ICommand NavegacionASalaDeChat { get; set; }
    public ICommand NavegacionARegistroDeUsuario { get; set; }
    public ICommand AbrirSalaDeChat { get; set; }
    public ICommand AbrirCrearSalaDeChat {get; set; }
    public ICommand AbrirRegistroDeUsuario {get; set; }
    public ICommand navegar { get; set; }


    // algo por revisar
    public ListaDeSalasVM(ICommand abrirCrearSalaDeChat,ICommand abrirSalaDeChat , ICommand abrirRegistroDeUsuario){
        AbrirCrearSalaDeChat = abrirCrearSalaDeChat;
        AbrirSalaDeChat = abrirSalaDeChat;
        AbrirRegistroDeUsuario = abrirRegistroDeUsuario;

        Salas = new ObservableCollection<Sala>();
       // navegar = new Comando(_ => abrirCrearSalaDeChat.Execute(_),() => abrirCrearSalaDeChat.CanExecute(null) );
    }

    public void AgregarSala(string nombre)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Salas.Add(new Sala { Nombre = nombre });
        });
    }

    public void SeleccionarSala(object sender, MouseEventArgs e)
    {

        var salaActual = sender as ListView;
        var salaSeleccionada = salaActual.SelectedItem as Sala;

            NavegacionASalaDeChat.Execute(salaSeleccionada);
    
    }

}