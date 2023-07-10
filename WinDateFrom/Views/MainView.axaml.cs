using Avalonia.Controls;
using DynamicData;
using System;
using Avalonia.Interactivity;
using System.IO;
using WinDateFrom.ViewModels;

namespace WinDateFrom.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        MainViewModel.CaricaOpzioni();
        data.SelectedDate = new DateTimeOffset(new DateTime(MainViewModel.GetAnno(), MainViewModel.GetMese(), MainViewModel.GetGiorno()));
        nome.Text = MainViewModel.GetNome();
    }

    private void Calcola_Click(object sender, RoutedEventArgs e)
    {
        risultato.Content = "";
        anniversario.Content = "";
        DateTime d = DateTime.Now;
        TimeSpan differenza = d - data.SelectedDate.Value;
        if (differenza.Milliseconds < 0)
        {
            risultato.Content = "Invalid rvalue";
            return;
        }
        if (differenza.Days > 1)
        {
            if (d.Day == data.SelectedDate.Value.Day)
                if (d.Month == data.SelectedDate.Value.Month)
                    anniversario.Content = "Is your anniversary";
                else
                    anniversario.Content = "Is your mesiversary";
        }
        if (nome.Text == "")
            risultato.Content = $"{differenza.Days} days are passed";
        else
            risultato.Content = $"You met {nome.Text} about {differenza.Days} days ago.";
        if (!MainViewModel.SalvaOpzioni(MainViewModel.path, nome.Text, data.SelectedDate.Value.Day, data.SelectedDate.Value.Month, data.SelectedDate.Value.Year))
            anniversario.Content = "Impossibile salvare le opzioni";
    }


}
