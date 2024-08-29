using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Chat.Modelo;

namespace Chat.VistaModelo;
public class SalaDeChatVM : BaseVM
{
    public ObservableCollection<Mensaje> Mensajes { get; set; }
    public ObservableCollection<Usuario> Usuarios { get; set; }
    public Usuario Usuario { get; set; }
    public Sala Sala
    {
        get { return _sala; }
        set
        {
            _sala = value;
            OnPropertyChanged(nameof(Sala));}
    }
    private Sala _sala;
    public ICommand NavegacionARegistroDeUsuario { get; set; }
    public ICommand NavegacionAListaDeSalas { get; set; }
    public ICommand NavegacionACrearSalaDeChat { get; set; }
    public ICommand vista { get; set; }
    public ICommand enviarMensaje { get; set; }
    public string MensajeTxt { get; set; }

    public SalaDeChatVM(ICommand abrirSalaDeChat, ICommand abrirListaDeSalas)
    {
        vista = new Comando(_ => abrirSalaDeChat.Execute(_));
        Mensajes = new ObservableCollection<Mensaje>();
        Usuarios = new ObservableCollection<Usuario>();
        enviarMensaje = new Comando(_ =>
        {
            var mensaje = new Mensaje { Texto = MensajeTxt, Usuario = Usuario, Fecha = DateTime.Now };
            Mensajes.Add(mensaje);
            MensajeTxt = "";
        });
    }

    public void AgregarUsuario(Usuario usuario)
    {
        if (Usuarios.Count == 0)
        {
            usuario.Host = Visibility.Visible;
        }
        else
        {
            usuario.Host = Visibility.Collapsed;
        }
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