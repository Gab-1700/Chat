using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Chat.Base.Modelo;
using System.Collections.ObjectModel;
using Chat.Cliente.Comandos;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Cliente.VistaModelo;

public class ListaDeSalasVM : BaseVM
{
    public ObservableCollection<Sala> Salas { get; set; }
    public Usuario Usuario { get; set; }
    public ICommand AbrirSalaDeChat { get; private set; }
    public ICommand AbrirCrearSalaDeChat { get; private set; }
    public ICommand AbrirRegistroDeUsuario { get; private set; }

    // algo por revisar
    public ListaDeSalasVM(AbrirRegistroDeUsuario abrirRegistroDeUsuario, AbrirCrearSala abrirCrearSala, AbrirSalaDeChat abrirSalaDeChat, HubConnection conexion, RegistroDeUsuarioVM registroDeUsuarioVM)
    {
        AbrirRegistroDeUsuario = abrirRegistroDeUsuario;
        AbrirCrearSalaDeChat = abrirCrearSala;
        AbrirSalaDeChat = new Comando((sala) =>
        {
            conexion.InvokeAsync("UnirASala", (sala as Sala).Id, registroDeUsuarioVM.Usuario);
            abrirSalaDeChat.Execute(sala);
        });

        Salas = new ObservableCollection<Sala>();
        conexion.On<List<Sala>>("RecibirSala", ActualizarSalas);   // recibo la sala del servidor como string  y lo agrego ami lista de salas 

    }

    private void ActualizarSalas(List<Sala> salas)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Salas.Clear();
            salas.ForEach(s => Salas.Add(s));
        });
    }

    public void EnviarUsuario()
    {


    }

}