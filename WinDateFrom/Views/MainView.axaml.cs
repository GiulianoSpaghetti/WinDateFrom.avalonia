using Avalonia.Controls;
using DynamicData;
using System;
using Avalonia.Interactivity;
using System.IO;
using WinDateFrom.ViewModels;

namespace WinDateFrom.Views;

public partial class MainView : UserControl
{
    private string ricorrenza="";
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
        ricorrenza=""
        if (differenza.Days > 1)
        {
            if (d.Day == data.SelectedDate.Value.Day)
                if (d.Month == data.SelectedDate.Value.Month) {
                    anniversario.Content = "Is your anniversary";
                    ricorrenza="anniversary";
               } else {
                    anniversario.Content = "Is your mesiversary";
                    ricorrenza="meiversary";
                }
        }
        anniversario.IsVisble=(ricorrenza!="" && nome!="")
        if (nome.Text == "")
            risultato.Content = $"{differenza.Days} days are passed";
        else
            risultato.Content = $"You met {nome.Text} about {differenza.Days} days ago.";
        if (!MainViewModel.SalvaOpzioni(MainViewModel.path, nome.Text, data.SelectedDate.Value.Day, data.SelectedDate.Value.Month, data.SelectedDate.Value.Year))
            anniversario.Content = "Impossibile salvare le opzioni";
    }

    private void Anniversario_Click(object sender, RoutedEventArgs e)
    {
            var psi = new ProcessStartInfo
            {
                FileName = $"https://twitter.com/intent/tweet?text=Happy {ricorrenza} my love.",
                UseShellExecute = true
            };
            fpShare.IsEnabled = false;
            Process.Start(psi);}
