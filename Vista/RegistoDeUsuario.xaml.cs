
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chat.Modelo;
using Chat.VistaModelo;

namespace Chat.Vista;
public partial class RegistroDeUsuario : UserControl
{

    public Usuario Usuario { get; set; }
    public ICommand NavegacionAListaDeSalas{ get; set; }
    public RegistroDeUsuario()
    {
        InitializeComponent();
        Usuario = new Usuario();
        DataContext = this;



        
        BitmapImage originalImage = new BitmapImage(new Uri(@"G:\Documents\Gabo\c#\Chat\Imagenes\Avatares.jpg", UriKind.RelativeOrAbsolute));
        int imageWidth = originalImage.PixelWidth;
        int imageHeight = originalImage.PixelHeight;
        CroppedBitmap croppedImage = new CroppedBitmap(originalImage, new Int32Rect(600, 600, 200, 200));
        //imagenRecortada.Source = croppedImage;

    }
        // falta implementar los avatares
}