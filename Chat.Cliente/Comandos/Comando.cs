using System.Windows;
using System.Windows.Input;

namespace Chat.Cliente.Comandos;

public class Comando : ICommand
{
    private readonly Action<object?>  _execute;
    private readonly Func<bool>? _canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public Comando(Action<object?> execute, Func<bool>? canExecute = null )
    {
        _canExecute = canExecute;
        _execute = execute;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute();

    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}