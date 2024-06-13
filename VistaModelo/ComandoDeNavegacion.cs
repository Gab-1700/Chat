using System.Windows;
using System.Windows.Input;

namespace Chat.VistaModelo;

public class ComandoDeNavegacion : ICommand
{
    private Action _action;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public ComandoDeNavegacion(Action action)
    {
        _action = action;
    }

    public bool CanExecute(object? parameter)
    {
        return _action != null;

    }

    public void Execute(object? parameter)
    {
        _action();
        //46
    }
}