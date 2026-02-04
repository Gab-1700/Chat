using System.ComponentModel;

namespace Chat.Cliente.VistaModelo;

public interface IVM : INotifyPropertyChanged
{
    void CargarModelo(object? modelo);

}