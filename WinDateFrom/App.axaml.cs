using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using WinDateFrom.ViewModels;
using WinDateFrom.Views;

namespace WinDateFrom;

public partial class App : Application
{
    public static bool IsDesktop = false;
    public override void Initialize()
    {
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture=new System.Globalization.CultureInfo("en-US");
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            IsDesktop = true;
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
