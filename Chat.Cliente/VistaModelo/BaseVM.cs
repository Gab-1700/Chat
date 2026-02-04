using System.ComponentModel;

namespace Chat.Cliente.VistaModelo;

public abstract class BaseVM : IVM
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void CargarModelo(object? modelo) { }
}