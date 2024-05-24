using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using Chat.Modelo;
using DrawingColor = System.Drawing.Color;
using MediaColor = System.Windows.Media.Color;

namespace Chat.Vista;
public partial class SalaDeChat : UserControl
{
    public SalaDeChat()
    {  
         InitializeComponent();
        
        var chats = new List<Mensaje>
        {
            new Mensaje() {Propietario= "Gabriel",Texto ="hola como estas",Fecha = DateTime.Now},
        };
        Chat.ItemsSource=chats;
        //Salas.ItemsSource=salas;
        //Chat
        
        var usuarios = new List<Usuario>{
            new Usuario() {Nombre="Antonio", Imagen = "AVATAR",Color= Colors.Red},
        };
        Usuarios.ItemsSource=usuarios;
    }
}