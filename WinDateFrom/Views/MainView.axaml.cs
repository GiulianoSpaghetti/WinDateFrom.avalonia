using Avalonia.Controls;
using DynamicData;
using System;
using Avalonia.Interactivity;
using System.IO;
using WinDateFrom.ViewModels;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia.Controls.ApplicationLifetimes;

namespace WinDateFrom.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        InitializeComponent();
    }


    private void Auguri_Click(object sender, RoutedEventArgs e)
    {
        Avalonia.Platform.Storage.ILauncher launcher = TopLevel.GetTopLevel(auguri).Launcher;
        launcher.LaunchUriAsync(new Uri($"https://twitter.com/intent/tweet?text=Happy%20{(DataContext as MainViewModel).ricorrenza}%20my%20love."));
        auguri.IsEnabled = false;
    }
}

