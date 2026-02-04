using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Chat.Cliente.Comandos;
using Chat.Base.Modelo;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.Cliente.VistaModelo;
// namespace Chat.Base.Modelo;
public class SalaDeChatVM : BaseVM
{

    public ObservableCollection<Mensaje> Mensajes { get; set; }
    public ObservableCollection<Usuario> Usuarios { get; set; }
    public ICommand NavegacionACrearSalaDeChat { get; set; }
    public ICommand NavegacionAListaDeSalas { get; set; }

    public Usuario Usuario { get; set; }
    public Sala Sala
    {
        get { return _sala; }
        set
        {
            _sala = value;
            OnPropertyChanged(nameof(Sala));
        }
    }
    private Sala _sala;
    public ICommand AbriRegistroDeUsuario { get; private set; }
    public ICommand AbrirListaDeSalas { get; private set; }
    public ICommand AbrirCrearSalaDeChat { get; private set; }
    public ICommand enviarMensaje { get; set; }
    public string MensajeTxt { get; set; }

    public SalaDeChatVM(AbrirRegistroDeUsuario abrirRegistroDeUsuario, AbrirCrearSala abrirCrearSala, AbrirListaDeSalas abrirListaDeSalas, RegistroDeUsuarioVM registroDeUsuarioVM, HubConnection conexion)
    {
        AbriRegistroDeUsuario = abrirRegistroDeUsuario;
        AbrirCrearSalaDeChat = abrirCrearSala;
        AbrirListaDeSalas = abrirListaDeSalas;
        Mensajes = new ObservableCollection<Mensaje>();
        Usuarios = new ObservableCollection<Usuario>();
        AgregarUsuario(registroDeUsuarioVM.Usuario);
        //aumente
        //conexion.On<List<Sala>>("RecibirSala",ActualizarUsuario);   // recibo la sala del servidor como string  y lo agrego ami lista de salas 

        enviarMensaje = new Comando(_ =>
        {
            var mensaje = new Mensaje { Texto = MensajeTxt, Usuario = Usuario, Fecha = DateTime.Now };
            Mensajes.Add(mensaje);
            MensajeTxt = "";
        });


        conexion.On<int, Usuario>("UnirASala", (salaId, usuario) =>
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Sala.Id == salaId)
                {
                    Usuarios.Add(usuario);
                }
            });
        });
    }

    public void AgregarUsuario(Usuario usuario)
    {
        usuario.EsAnfitrion = Usuarios.Count == 0;
        Usuarios.Add(usuario);
    }

    public override void CargarModelo(object? modelo)
    {

        if (modelo is Sala sala)
        {
            Sala = sala;
            //OnPropertyChanged(nameof(Sala));
        }

    }





}