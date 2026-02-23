using System.ComponentModel;

namespace Chat.Cliente.VistaModelo;

public interface IVM<TArgs> : INotifyPropertyChanged
{
    Task LoadAsync(TArgs? args);
}