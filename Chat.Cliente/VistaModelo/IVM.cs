using System.ComponentModel;

namespace Chat.VistaModelo;

public interface IVM : INotifyPropertyChanged
{
    void CargarModelo(object? modelo);

}