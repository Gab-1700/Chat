using System.Windows.Input;
using Chat.Cliente.Comandos;
using Chat.Base.Modelo;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;

namespace Chat.Cliente.VistaModelo;

public class CrearSalaVM : BaseVM
{
    public Sala Sala { get; set; }

    public Usuario Usuario { get; set; }

    public ICommand AbrirSalaDeChat { get; private set; }
    public ICommand AbrirListaDeSalas { get; private set; }


    public CrearSalaVM(AbrirListaDeSalas abrirListaDeSalas, AbrirSalaDeChat abrirSalaDeChat, HubConnection conexion)
    {
        // navegar = new Comando(_ => abriSalaDeChat.Execute(_),PuedesNavegar);
        AbrirListaDeSalas = abrirListaDeSalas;
        AbrirSalaDeChat = new Comando(_ =>
        {
            conexion.InvokeAsync("CargarSala", Sala);
            abrirSalaDeChat.Execute(Sala);
        }, PuedesNavegar);
        Sala = new Sala();
        conexion.On<int>("GuardarSalaId", (salaId) =>
        {
            Application.Current.Dispatcher.Invoke(() => Sala.Id = salaId);
        });

    }


    public bool PuedesNavegar()
    {
        return !(Sala.Nombre == null || Sala.Nombre == "");
    }

}