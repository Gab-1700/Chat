using System.Windows.Controls;
using System.Windows.Input;
using Chat.Cliente.VistaModelo;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Cliente.Comandos;

public abstract class AbrirVista<T> : ICommand where T : UserControl
{
    private IServiceProvider _proveedor;

    public AbrirVista(IServiceProvider proveedor)
    {
        _proveedor = proveedor;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {

        var mainWindowVM = _proveedor.GetService<MainWindowVM>();
        var vista = _proveedor.GetRequiredService<T>();
        mainWindowVM.VistaActual = vista;

        if (vista.DataContext is IVM vm)
        {
            vm.CargarModelo(parameter);
        }
    }
}