using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using System.Globalization;
using WinDateFrom.ViewModels;
using WinDateFrom.Views;

namespace WinDateFrom;

public partial class App : Application
{
    public static bool IsDesktop = false;
    public override void Initialize()
<<<<<<< HEAD
    {
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture=new System.Globalization.CultureInfo("en-US");
=======
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

>>>>>>> 647e5c7d96c9a9b1b5879bc87219cb768ede2441
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
            IsDesktop = true;
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
