using System.ComponentModel;
using System.Windows.Controls;
using Chat.Vista;

namespace Chat.VistaModelo;

public class MainWindowVM : INotifyPropertyChanged
{
    private UserControl _vistaActual;
    private RegistroDeUsuario _registroDeUsuario;
    private ListaDeSalas _listaDeSalas;
    private SalaDeChat _salaDeChat;
    private CrearSala _crearSala;
    public UserControl VistaActual
    {
        get { return _vistaActual; }
        set
        {
            _vistaActual = value;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(VistaActual)));
        }
    }

   //public void num() {Console.WriteLine(5);}
   //  ees equivalente a esto 
   //() => Console.WriteLine(5)
   // () => {Console.WriteLine(5);} 
    public MainWindowVM()
    {
        var navegacionASalaDeChat = new ComandoDeNavegacion(() => VistaActual = _salaDeChat);
        var navegacionACrearSalaDeChat = new ComandoDeNavegacion(() => VistaActual = _crearSala);
        var navegacionARegistroDeUsuario = new ComandoDeNavegacion(() => VistaActual = _registroDeUsuario);
        var navegacionAListaDeSalas = new ComandoDeNavegacion(() => VistaActual = _listaDeSalas);

        //atributos
        _registroDeUsuario = new RegistroDeUsuario() { NavegacionAListaDeSalas = navegacionAListaDeSalas };
        _listaDeSalas = new ListaDeSalas()
        {
            Usuario = _registroDeUsuario.Usuario,
            NavegacionACrearSala = navegacionACrearSalaDeChat,
            NavegacionARegistroDeUsuario = navegacionARegistroDeUsuario
        };

        _crearSala = new CrearSala()
        {
            Usuario = _registroDeUsuario.Usuario,
            NavegacionAListaDeSalas = navegacionAListaDeSalas,
            NavegacionARegistroDeUsuario = navegacionARegistroDeUsuario,
            NavegacionASalaDeChat = navegacionASalaDeChat
        };

        _salaDeChat = new SalaDeChat()
        {
            Usuario = _registroDeUsuario.Usuario,
            Sala = _crearSala.Sala,
            NavegacionACrearSalaDeChat = navegacionACrearSalaDeChat,
            NavegacionARegistroDeUsuario = navegacionARegistroDeUsuario,
            NavegacionAListaDeSalas = navegacionAListaDeSalas
        };

        //comandos

        _vistaActual = _registroDeUsuario;
    }
   

    public event PropertyChangedEventHandler? PropertyChanged;

}