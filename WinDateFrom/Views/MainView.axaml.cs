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
    private static string ricorrenza = "";
    public MainView()
    {
        InitializeComponent();
        MainViewModel.CaricaOpzioni();
        data.SelectedDate = new DateTimeOffset(new DateTime(MainViewModel.GetAnno(), MainViewModel.GetMese(), MainViewModel.GetGiorno()));
        nome.Text = MainViewModel.GetNome();
    }

    private void Calcola_Click(object sender, RoutedEventArgs e)
    {
        calcola.IsEnabled = false;
        nome.Text = nome.Text.Trim();
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - data.SelectedDate.Value;
        if (differenza.Milliseconds < 0)
        {
            risultato.Content = "Invalid rvalue";
            return;
        }
        ricorrenza = "";
        if (differenza.Days > 1)
        {
            if (d.Day == data.SelectedDate.Value.Day)
                if (d.Month == data.SelectedDate.Value.Month)
                {
                    anniversario.Content = "Is your anniversary";
                    ricorrenza = "anniversary";
                }
                else
                {
                    anniversario.Content = "Is your mesiversary";
                    ricorrenza = "mesiversary";
                }
        }
        if (nome.Text == "")
            risultato.Content = $"{differenza.Days} days are passed";
        else
            risultato.Content = $"You met {nome.Text} about {differenza.Days} days ago.";
        if (!MainViewModel.SalvaOpzioni(nome.Text, data.SelectedDate.Value.Day, data.SelectedDate.Value.Month, data.SelectedDate.Value.Year))
            anniversario.Content = "Impossibile salvare le opzioni";
        auguri.IsVisible = anniversario.IsVisible = ricorrenza != "" && nome.Text != "";
        calcola.IsEnabled = false;
    }

    private void Auguri_Click(object sender, RoutedEventArgs e)
    {
        if (!App.IsDesktop)
            risultato.Content = "Operation currently not supported";
        else
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = $"https://twitter.com/intent/tweet?text=Happy%20{ricorrenza}%20my%20love.",
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        auguri.IsEnabled = false;
    }
}
