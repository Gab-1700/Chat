using System.Windows.Controls;
using Chat.Cliente.VistaModelo;

namespace Chat.Vista;
public partial class RegistroDeUsuario : UserControl
{
    public RegistroDeUsuario(RegistroDeUsuarioVM registroDeUsuarioVM)
    {
        InitializeComponent();
        DataContext = registroDeUsuarioVM;

       // BitmapImage originalImage = new BitmapImage(new Uri(@"G:\Documents\Gabo\c#\Chat\Imagenes\Avatares.jpg", UriKind.RelativeOrAbsolute));
        //int imageWidth = originalImage.PixelWidth;
        //int imageHeight = originalImage.PixelHeight;
        //CroppedBitmap croppedImage = new CroppedBitmap(originalImage, new Int32Rect(600, 600, 200, 200));
        //imagenRecortada.Source = croppedImage;
    }
}