using System.ComponentModel;

namespace Chat.Cliente.VistaModelo;

public abstract class BaseVM<TArgs> : IVM<TArgs>
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual Task LoadAsync(TArgs? args)
    {
        return Task.CompletedTask;   
    }
}