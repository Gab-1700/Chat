using System.Windows.Controls;
using Chat.Cliente.VistaModelo;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Services;

public class NavigationService(IServiceProvider provider)
{
    public void NavigateTo<TView>() where TView : UserControl
    {
        NavigateTo<TView, object>(null);
    }

    public void NavigateTo<TView, TArgs>(TArgs? args) where TView : UserControl
    {
        var mainWindowVM = provider.GetService<MainWindowVM>();
        var view = provider.GetRequiredService<TView>();
        mainWindowVM?.VistaActual = view;

        if (view.DataContext is IVM<TArgs> vm)
        {
            vm.LoadAsync(args);
        }
    }
}
