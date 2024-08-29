using System.Windows;
using System.Windows.Media;

namespace Chat.Modelo;

public class Usuario
{

    public string Nombre { get; set; }
    public string Imagen { get; set; }
    public Brush Color { get; set; }
    public Visibility Host { get; set; }



}